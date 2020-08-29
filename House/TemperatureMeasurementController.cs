using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using itec_mobile_api_final.Data;
using itec_mobile_api_final.Extensions;
using itec_mobile_api_final.Heater;
using itec_mobile_api_final.HeaterSchedule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
//using Microsoft.ML.Data;
//using Microsoft.ML;
//using Microsoft.ML.Data;
using Newtonsoft.Json;
namespace itec_mobile_api_final.Heater
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    public class TemperatureMeasurementController : Controller
    {
        MLContext mlContext = new MLContext(seed: 0);
        private readonly IRepository<TemperatureMeasurementEntity> _tempMeasurementRepository;
        private readonly IRepository<HeaterEntity> __heaterRepository;
        private readonly IRepository<HeaterScheduleEntity> _heaterScheduleRepository;
        private String API = "6cd7476006dfdddfb4b44734f6db3744";
        private String LAT = "46.4167";
        private String LON = "21.95";

        public TemperatureMeasurementController(ApplicationDbContext context)
        {
            _tempMeasurementRepository = context.GetRepository<TemperatureMeasurementEntity>();
            __heaterRepository = context.GetRepository<HeaterEntity>();
            _heaterScheduleRepository = context.GetRepository<HeaterScheduleEntity>();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TemperatureMeasurementEntity measurement)
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
            String response;
            String res;

            measurement.UserId = userId;
            measurement.Id = Guid.NewGuid().ToString();
            measurement.OutsideTemperature = 55;
            measurement.MeasurementTime = DateTime.Now;
            try
            {
                String url =
                    "https://api.openweathermap.org/data/2.5/weather?lat=" + LAT + "&lon=" + LON +
                    "&units=metric&appid=" + API;
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(url);
                    res = response;
                    dynamic dyn = JsonConvert.DeserializeObject(res);
                    float temp = dyn.main.temp;
                    measurement.OutsideTemperature = (int) temp;

                }
            }
            catch
            {
                Console.WriteLine("eroare la vreme");
            }
            
            var existing =  __heaterRepository.Queryable.FirstOrDefault(t => t.UserId == userId);
            if (existing == null)
                return NoContent();
            
            existing.Temperature = measurement.HouseTemperature;
            existing.IsOn = measurement.HeaterTemperature > 30;
            await __heaterRepository.UpdateAsync(existing);
            await _tempMeasurementRepository.AddAsync(measurement);
            return Ok();
        }

        
    
        [HttpPost("/TrainModel")]
        [ProducesResponseType(typeof(HeaterEntity), 200)]
        public async Task<IActionResult> TrainModel(int temperature)
        {  
            
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
            
            

            var measurements = await PrepareMeasurement(userId);
            
            var model = Train(mlContext, measurements);
//            Evaluate(mlContext, model, measurements);
//            TestPrediction(mlContext, model);
            int time =(int)PredictTime(mlContext, model, temperature);
            return Ok(time);
        }
        [HttpPost("/Schedule")]
        [ProducesResponseType(typeof(HeaterEntity), 200)]
        public async Task<IActionResult> Schedule([FromBody]HeaterSchedule heaterSchedule)
        {  
            
            String response;
            String res;

            

            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();

            var dateNow = DateTime.Now;
            if(dateNow> new DateTime(dateNow.Year,dateNow.Month,dateNow.Day,heaterSchedule.Hour,heaterSchedule.Minute,0))
                dateNow= dateNow.AddDays(1);
            var heaterScheduleEntity = new HeaterScheduleEntity()
            {
                FinalHouseTemperature = heaterSchedule.FinalHouseTemperature,
                HeaterFinishedTime = new DateTime(dateNow.Year,dateNow.Month,dateNow.Day,heaterSchedule.Hour,heaterSchedule.Minute,0),
                UserId = userId
            };
            try
            {
                String url =
                    "https://api.openweathermap.org/data/2.5/weather?lat=" + LAT + "&lon=" + LON +
                    "&units=metric&appid=" + API;
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(url);
                    res = response;
                    dynamic dyn = JsonConvert.DeserializeObject(res);
                    float temp = dyn.main.temp;
                    heaterScheduleEntity.FinalOutsideTemperature = temp;
                    heaterScheduleEntity.OutsideAverageTemperature = temp;
                    heaterScheduleEntity.InitialOutsideTemperature = temp;
                    heaterScheduleEntity.InitialHouseTemperature = 17;


                }
            }
            catch
            {

            }
            var measurements = await PrepareMeasurement(userId);
            
            var model = Train(mlContext, measurements);
            heaterScheduleEntity.HeatingTime =(int)PredictTimeComplex(mlContext, model, heaterScheduleEntity);
            heaterScheduleEntity.HeaterStartTime =
                heaterScheduleEntity.HeaterFinishedTime.AddSeconds(-(int)heaterScheduleEntity.HeatingTime);
           
           await _heaterScheduleRepository.AddAsync(heaterScheduleEntity);
            return Ok(heaterScheduleEntity.HeaterStartTime);
        }


        public static ITransformer Train(MLContext mlContext, List<HeatingProcess> measurements)
        {
            IDataView dataView = mlContext.Data.LoadFromEnumerable(measurements);
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName:"HeatingTime")
                .Append(mlContext.Transforms.Concatenate("Features", "InitialHouseTemperature", "InitialOutsideTemperature", "FinalOutsideTemperature", "FinalHouseTemperature","OutsideAverageTemperature", "HeaterAverageTemperature"))
                .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression());
            var model = pipeline.Fit(dataView);
            return model;
        }
        
        private static void Evaluate(MLContext mlContext, ITransformer model, List<HeatingProcess> measurments)
        {
            IDataView dataView = mlContext.Data.LoadFromEnumerable(measurments);
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "HeatingTime");
            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
            
            
            
        }
        private static float PredictTime(MLContext mlContext, ITransformer model, int temperature)
        {
          
            var predictionFunction = mlContext.Model.CreatePredictionEngine<HeatingProcess, HeatingProcessPrediction>(model);
            var heatingProcessSample = new HeatingProcess()
            {
                
                InitialHouseTemperature = 17, 
                InitialOutsideTemperature  = 7, 
                FinalOutsideTemperature = 8,
                FinalHouseTemperature  = temperature,
                HeaterAverageTemperature = 55,
                OutsideAverageTemperature  = 8,
                HeatingTime = 0

                
            };
            var prediction = predictionFunction.Predict(heatingProcessSample);
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.HeatingTime:0.####}");
            Console.WriteLine($"**********************************************************************");
            return prediction.HeatingTime;
        }
        
        private static float PredictTimeComplex(MLContext mlContext, ITransformer model, HeaterScheduleEntity heatingScheduleSample )
        {
          
            var predictionFunction = mlContext.Model.CreatePredictionEngine<HeatingProcess, HeatingProcessPrediction>(model);
            var heatingProcessSample = new HeatingProcess()
            {
                
                InitialHouseTemperature = heatingScheduleSample.InitialHouseTemperature, 
                InitialOutsideTemperature  = heatingScheduleSample.InitialOutsideTemperature, 
                FinalOutsideTemperature = heatingScheduleSample.FinalOutsideTemperature,
                FinalHouseTemperature  = heatingScheduleSample.FinalHouseTemperature,
                HeaterAverageTemperature = 55,
                OutsideAverageTemperature  = heatingScheduleSample.OutsideAverageTemperature,
                HeatingTime = 0

                
            };
            var prediction = predictionFunction.Predict(heatingProcessSample);
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.HeatingTime:0.####}");
            Console.WriteLine($"**********************************************************************");
            return prediction.HeatingTime;
        }
        private static void TestPrediction(MLContext mlContext, ITransformer model)
        {
          
            var predictionFunction = mlContext.Model.CreatePredictionEngine<HeatingProcess, HeatingProcessPrediction>(model);
            var heatingProcessSample = new HeatingProcess()
            {
                
                InitialHouseTemperature = 18, 
                InitialOutsideTemperature  = 10, 
                FinalOutsideTemperature = 10,
                FinalHouseTemperature  = 21,
                HeaterAverageTemperature = 55,
                OutsideAverageTemperature  = 10,
                HeatingTime = 0

                
            };
            var prediction = predictionFunction.Predict(heatingProcessSample);
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.HeatingTime:0.####}");
            Console.WriteLine($"**********************************************************************");
        }
        private async Task<List<HeatingProcess>> PrepareMeasurement(String userId)
        {
            var heatingProcesses = new List<HeatingProcess>(); 
            var existing = await _tempMeasurementRepository.Queryable.Where(t =>
                t.UserId == userId).OrderBy(entity => entity.MeasurementTime).ToListAsync();
            try
            {
                bool newDay = true;
                DateTime oldDay = new DateTime(0);
                int oldHeaterTemperature = 0;
                DateTime intialDateTime = new DateTime(0);
                int heaterTemperatureSum = 0;
                int outsideTemperatureSum = 0;
                int heaterTemperatureMeasurementsNo = 0;
                var heatingProcess= new HeatingProcess();
                bool tempRegistered20 = false;
                bool tempRegistered21 = false;
                bool tempRegistered22 = false;
                bool tempRegistered23 = false;
                int stage20 = 0;
                int stage21 = 0;
                int stage22 = 0;
                int stage23 = 0;
                
                foreach (var measurment in existing)
                {  
                    newDay = measurment.MeasurementTime.DayOfYear != oldDay.DayOfYear;
                  
                        
                        if (newDay && measurment.MeasurementTime.Hour> 10 &&measurment.HeaterTemperature > 30 && oldHeaterTemperature < 30 && measurment.HouseTemperature<20)
                        {
                            stage20 = 1;
                            intialDateTime = measurment.MeasurementTime;
                            heaterTemperatureSum = measurment.HeaterTemperature;
                            outsideTemperatureSum = measurment.OutsideTemperature;
                           
                            heatingProcess.InitialHouseTemperature = measurment.HouseTemperature;
                            heatingProcess.InitialOutsideTemperature = measurment.OutsideTemperature;
                            tempRegistered20 = false;
                             tempRegistered21 = false;
                             tempRegistered22 = false;
                             tempRegistered23 = false;
                             heaterTemperatureMeasurementsNo = 1;
//                             Console.WriteLine("temperatura cand porneste centrala: "+ heatingProcess.InitialHouseTemperature+ " "+ intialDateTime);
                        }else if (!tempRegistered20 &&measurment.HouseTemperature ==20 )
                    {
                        heaterTemperatureMeasurementsNo++;
                        heaterTemperatureSum += measurment.HeaterTemperature;
                        outsideTemperatureSum += measurment.OutsideTemperature;
                        tempRegistered20 = true;
                        heatingProcess.FinalHouseTemperature = 20;
                        heatingProcess.FinalOutsideTemperature = measurment.OutsideTemperature;
                        heatingProcess.HeatingTime = (int)(measurment.MeasurementTime - intialDateTime).TotalSeconds;
                        heatingProcess.HeaterAverageTemperature =
                            heaterTemperatureSum / heaterTemperatureMeasurementsNo;
                        heatingProcess.OutsideAverageTemperature =
                            outsideTemperatureSum / heaterTemperatureMeasurementsNo;
                        HeatingProcess heating = (HeatingProcess) heatingProcess.Clone();
                        heatingProcesses.Add(heating);
//                        Console.WriteLine(heaterTemperatureMeasurementsNo+" "+ heaterTemperatureSum);
                        stage20 = 0;

                    }
                        else if (!tempRegistered21 &&measurment.HouseTemperature ==21 )
                        {
                            heaterTemperatureMeasurementsNo++;
                            heaterTemperatureSum += measurment.HeaterTemperature;
                            outsideTemperatureSum += measurment.OutsideTemperature;
                            tempRegistered21 = true;
                            heatingProcess.FinalHouseTemperature = 21;
                            heatingProcess.FinalOutsideTemperature = measurment.OutsideTemperature;
                            heatingProcess.HeatingTime = (int)(measurment.MeasurementTime - intialDateTime).TotalSeconds;
                            heatingProcess.HeaterAverageTemperature =
                                heaterTemperatureSum / heaterTemperatureMeasurementsNo;
                            heatingProcess.OutsideAverageTemperature =
                                outsideTemperatureSum / heaterTemperatureMeasurementsNo;
                            HeatingProcess heating = (HeatingProcess) heatingProcess.Clone();
                            heatingProcesses.Add(heating);
//                        Console.WriteLine(heaterTemperatureMeasurementsNo+" "+ heaterTemperatureSum);
                            stage21 = 0;

                        }
                        else if (!tempRegistered22 &&measurment.HouseTemperature ==22 )
                        {
                            heaterTemperatureMeasurementsNo++;
                            heaterTemperatureSum += measurment.HeaterTemperature;
                            outsideTemperatureSum += measurment.OutsideTemperature;
                            tempRegistered22 = true;
                            heatingProcess.FinalHouseTemperature = 22;
                            heatingProcess.FinalOutsideTemperature = measurment.OutsideTemperature;
                            heatingProcess.HeatingTime = (int)(measurment.MeasurementTime - intialDateTime).TotalSeconds;
                            heatingProcess.HeaterAverageTemperature =
                                heaterTemperatureSum / heaterTemperatureMeasurementsNo;
                            heatingProcess.OutsideAverageTemperature =
                                outsideTemperatureSum / heaterTemperatureMeasurementsNo;
                            HeatingProcess heating = (HeatingProcess) heatingProcess.Clone();
                            heatingProcesses.Add(heating);
//                        Console.WriteLine(heaterTemperatureMeasurementsNo+" "+ heaterTemperatureSum);
                            stage22 = 0;

                        }
                        else if (!tempRegistered23 &&measurment.HouseTemperature ==23 )
                        {
                            heaterTemperatureMeasurementsNo++;
                            heaterTemperatureSum += measurment.HeaterTemperature;
                            outsideTemperatureSum += measurment.OutsideTemperature;
                            tempRegistered23 = true;
                            heatingProcess.FinalHouseTemperature = 23;
                            heatingProcess.FinalOutsideTemperature = measurment.OutsideTemperature;
                            heatingProcess.HeatingTime = (int)(measurment.MeasurementTime - intialDateTime).TotalSeconds;
                            heatingProcess.HeaterAverageTemperature =
                                heaterTemperatureSum / heaterTemperatureMeasurementsNo;
                            heatingProcess.OutsideAverageTemperature =
                                outsideTemperatureSum / heaterTemperatureMeasurementsNo;
                            HeatingProcess heating = (HeatingProcess) heatingProcess.Clone();
                            heatingProcesses.Add(heating);
//                        Console.WriteLine(heaterTemperatureMeasurementsNo+" "+ heaterTemperatureSum);
                            stage23 = 0;

                        }
                    else if (stage20 ==1 || stage21==1 || stage22==1 || stage23==1)
                    {
                        outsideTemperatureSum += measurment.OutsideTemperature;
                        heaterTemperatureSum += measurment.HeaterTemperature;
                        heaterTemperatureMeasurementsNo++;
                    }

                    oldHeaterTemperature = measurment.HeaterTemperature;

                }
            }
             catch
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!aia e");
            }

            foreach (var heatingProcess in heatingProcesses)
            {
                Console.Write("initial temp "+ heatingProcess.InitialHouseTemperature);
                Console.WriteLine(" heating time "+ heatingProcess.HeatingTime+"temp: "+ heatingProcess.FinalHouseTemperature+  " avg  outside "+ heatingProcess.OutsideAverageTemperature+ " heater avg "+ heatingProcess.HeaterAverageTemperature);

            }

            return heatingProcesses;

        }

        public class HeatingProcess
        {
            [LoadColumn(0)]
            public float InitialHouseTemperature { get; set; }
            [LoadColumn(1)]
            public float InitialOutsideTemperature { get; set; }
            [LoadColumn(2)]
            public float FinalOutsideTemperature { get; set; }
            [LoadColumn(3)]
            public float FinalHouseTemperature { get; set; }
            [LoadColumn(4)]
            public float HeaterAverageTemperature { get; set; }
            [LoadColumn(6)]
            public float OutsideAverageTemperature { get; set; }
            [LoadColumn(7)]
            public float HeatingTime { get; set; }
            
            public object Clone()
            {
                return this.MemberwiseClone();
            }

        }

        public class HeatingProcessPrediction
        {
            [ColumnName("Score")]
            public float HeatingTime { get; set; }
        }


        public class HeaterSchedule
        {
            public int Minute { get; set; }
            public  int Hour { get; set; }
            public float FinalHouseTemperature { get; set; }
        }

}
}
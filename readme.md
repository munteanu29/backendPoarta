
## API.StartApp - .Net Core Web API App


### Setup
   * Install ``NET Core SDK 2.2``
   * Install/Configure a ``MariaDB >= 10.3`` database (version 10.3 or higher)
   * Clone this repo 
     * Make sure the ``API.Base`` submodule is cloned (see submodule options in your Git GUI tool or run ``git submodule update --init --recursive``)
   * Set your environment variables (inside ``API.StartApp`` project directory)
       * Create ``conf.vars.local`` file and overwrite environment variables you want to have different values from ``conf.vars``
       * Run ``./set-env-vars.sh`` (Linux) / ``set-env-vars.bat`` (Windows) (use ``CMD`` and NOT ``PowerShell``) 
   * Update and Seed database: ``dotnet run --seed true --migrate true`` (inside ``API.StartApp`` project directory)

### Dev
   * Run:
        * Linux: ``./run-dev.sh``
        * Windows: ``run-dev.bat``
   * Adding new model+api (don't forget to change the name of the entity in your commands)
        * Add ``CustomEntity`` in ``YourProject/Models/Entity`` inheriting from ``API.Base/API.Base.Web.Base/Models/Entities/Entity``
            * This model is linked to the database table.
        * Add ``CustomViewModel`` in ``YourProject/Models/ViewModels`` inheriting from ``API.Base/API.Base.Web.Base/Models/ViewModels/ViewModel``
            * This ViewModel is used in api for json parsing/serializing.
        * Add ``CustomEntityMap`` in ``YourProject/Models/EntityMaps``
            * This EntityMap links the Entity and the ViewModel in AutoMapper and tells EntityFramework this Entity should be linked into database.
        * Generate Migration: ``dotnet ef migrations add migration_name``
        * Update Database: ``dotnet ef database update``
        
   * New admin UI for new model
     * Check this section in ``API.Base`` Readme. (The ``readme.md`` file in ``API.Base`` directory/repository).   
 
### Deploy/Access
   * access 
        * ``/api/docs`` for swagger 
        * ``/api/[controller]/[action]`` for api
        * ``/api/admin`` for the WIP admin panel
   * if deployed under proxy those paths must be proxied:
     * ``/api*``
     * ``/Identity*``
     
### Environment variables
   * Check this section in ``API.Base`` Readme. (The ``readme.md`` file in ``API.Base`` directory/repository).  
# studentsBackend


##instrucciones para ejecutar backend:

La bd se realizó usando entity framework, con migrations
pasos para generar y popular la BD con los datos de prueba de materias, profesores:
actrualizar el archivo appsettings.json dentro del proyecto Students.API con el valor de la conexión al servidor que se usará:
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=StudentDb;Trusted_Connection=True;" 

para generar la BD se debe abrir el proyecto y en el directiorio raiz del API ejecutar:
        Update-Database — desde el package manager console
                o desde consola:
        dotnet ef database update — desde consola 
ejecutando este comando se creara la base de datos

y para hacer populate de los datos de prueba de materias y profesores solo se debe ejecutar el proyecto del backend y automaticamente se realiza el seed de estos datos:
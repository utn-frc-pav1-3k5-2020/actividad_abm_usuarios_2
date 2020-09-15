
# BugTracker - ABM Usuarios 2da Parte


## 1. Clonar Repositorio (Clone/Checkout)

**1.1. Ejecutar comando clone para descargar repositorio:** 
```sh
$ git clone https://github.com/utn-frc-pav1-3k5-2020/actividad_abm_usuarios_2.git
```
**1.2. Ubicarse en la carpeta generada con el nombre del repositorio: 

```sh
$ cd actividad_abm_usuarios_2
```

**1.3. Crear un nuevo branch (rama)**

Para crear una nueva rama (branch) y saltar a ella, en un solo paso, puedes utilizar el comando  `git checkout`  con la opción  `-b`, indicando el nombre del nuevo branch (reemplazando el nro de legajo) de la siguiente forma branch_{legajo}, para el legajo 12345:

```sh
$ git checkout -b branch_12345 
Switched to a new branch "12345"
```
Y para que se vea reflejada en GitHub:
```sh
$ git push --set-upstream origin branch_12345
```

## 2. Temas

**2.1. Manejo de Excepciones**

Cuando se inicia una excepción, el entorno runtime comprueba la instrucción actual para ver si se encuentra dentro de un bloque  `try`.  Si es así, se comprueban los bloques  `catch`  asociados al bloque  `try`  para ver si pueden detectar la excepción.  Los bloques  `Catch`  suelen especificar tipos de excepción; si el tipo del bloque  `catch`  es el mismo de la excepción, o una clase base de la excepción, el bloque  `catch`  puede controlar el método.  Por ejemplo:

```csharp
static void TestCatch()
{
    try
    {
        TestThrow();
    }
    catch (CustomException ex)
    {
        System.Console.WriteLine(ex.ToString());
    }
}

```

En general con tres de las propiedades de la excepción suele haber suficiente información como para saber la causa del error y poder corregirlo:
-  ***MessageError***: proporciona una explicación de lo que ha ocurrido.  
    
- ***InnerException***: proporciona información sobre la excepción interna.  
    
- ***StackTrace***: proporciona información de la pila de llamadas antes de la excepción.

Una instrucción  `try`  puede contener más de un bloque  `catch`.  Se ejecuta la primera instrucción  `catch`  que pueda controlar la excepción; las instrucciones  `catch`  siguientes se omiten, aunque sean compatibles.  Por consiguiente, los bloques catch deben ordenarse siempre de más específico (o más derivado) a menos específico.  Por ejemplo:
```csharp
using System;
using System.IO;

public class ExceptionExample
{
    static void Main()
    {
        try
        {
            using (var sw = new StreamWriter(@"C:\test\test.txt"))
            {
                sw.WriteLine("Hello");
            }   
        }
        // Put the more specific exceptions first.
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine(ex);  
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex);  
        }
        // Put the least specific exception last.
        catch (IOException ex)
        {
            Console.WriteLine(ex);  
        }

        Console.WriteLine("Done"); 
    }
}
```
Para que el bloque  `catch`  se ejecute, el entorno runtime busca bloques  `finally`.  Los bloques  `Finally`  permiten al programador limpiar cualquier estado ambiguo que pudiera haber quedado tras la anulación de un bloque  `try`  o liberar los recursos externos (como identificadores de gráficos, conexiones de base de datos o flujos de archivos) sin tener que esperar a que el recolector de elementos no utilizados del entorno runtime finalice los objetos.  Por ejemplo:
```csharp
static void TestFinally()
{
    System.IO.FileStream file = null;
    //Change the path to something that works on your machine.
    System.IO.FileInfo fileInfo = new System.IO.FileInfo(@"C:\file.txt");

    try
    {
        file = fileInfo.OpenWrite();
        file.WriteByte(0xF);
    }
    finally
    {
        // Closing the file allows you to reopen it immediately - otherwise IOException is thrown.
        if (file != null)
        {
            file.Close();
        }
    }

    try
    {
        file = fileInfo.OpenWrite();
        System.Console.WriteLine("OpenWrite() succeeded");
    }
    catch (System.IO.IOException)
    {
        System.Console.WriteLine("OpenWrite() failed");
    }
}
```
Las excepciones están representadas por clases derivadas de  [Exception](https://docs.microsoft.com/es-es/dotnet/api/system.exception).  Esta clase identifica el tipo de excepción y contiene propiedades que tienen los detalles sobre la excepción.  Iniciar una excepción implica crear una instancia de una clase derivada de excepción, configurar opcionalmente las propiedades de la excepción y luego producir el objeto con la palabra clave  `throw`.  Por ejemplo:

```csharp
	class CustomException : Exception
       {
           public CustomException(string message)
           {
              
           }

       }
       private static void TestThrow()
       {
           CustomException ex =
               new CustomException("Custom exception in TestThrow()");

           throw ex;
       }
```

**2.2. Archivo de Configuración (app.config)**

En su forma más simple, app.config es un archivo XML con muchas secciones de configuración predefinidas disponibles y soporte para secciones de configuración personalizadas. Una "sección de configuración" es un fragmento de XML con un esquema destinado a almacenar algún tipo de información.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
  <appSettings>
    <add key="dataBaseName" value="local"/>
  </appSettings>
  <connectionStrings>
    <add name="local" connectionString="Data Source=.\\SQLEXPRESS;Initial Catalog=BugTracker;Integrated Security=true;"/>
  </connectionStrings>
</configuration>

```

```csharp
	string setting = ConfigurationManager.AppSettings["dataBaseName"];
    string conn = ConfigurationManager.ConnectionStrings[setting].ConnectionString;
```

## 3. Actividades
> Iniciar la solución que se encuentra en la carpeta src/BugTracker.sln

**3.1 Sobre frmABMUsuario agregar manejo excepciones**

Agregar try...catch en el evento click del boton aceptar para controlar cualquier error y mostrar un mensaje al usuario.

**3.2 Agregar ConnectionString como configuración en app.config**


## 4. Versionar en GitHub los cambios locales (add / commit / push)

> A continuación vamos a crear el **commit** y subir los cambios al servidor GitHub.

4.1. **Status**. Verificamos los cambios pendientes de versionar.

```sh
$ git status
```

4.2. **Add**. Agregamos todos los archivos nuevos no versionados.

```sh
$ git add -A
```

4.3. **Commit**. Generamos un commit con todos los cambios y agregamos un comentario.

```sh
$ git commit -a -m "Comentario"
```

4.4. **Push**. Enviamos todos los commits locales a GitHub

```sh
$ git push
```

4.5. **Status**. Verificar que no quedaron cambios pendientes de versionar

```sh
$ git status
```

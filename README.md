Minificador
===========

Minificador de JavaScript y CSS.

---
Antes de ejecutar la aplicación tienen que configurar la ruta raíz de su aplicación web en LogiStudio.Paradise.Minifier.exe.config, en el campo 
value = "Ruta de su aplicación" y ejecutar el .exe. La aplicación buscará todos los Css y Js y los minificará.
El primer proceso de "Creating Temp Folder" puede tardar un poco dependiendo del tamaño de su aplicación

```
<?xml version="1.0" encoding="utf-8" ?><configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
  <appSettings>
    <add key="RootFolder" value="C:\Proyectos\LogicStudio\Paradise-v1.1\Paradise\LogicStudio.Paradise\Publish\"/>
  </appSettings>
</configuration>
```

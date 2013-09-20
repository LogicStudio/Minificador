Minificador
===========

Minificador de JavaScript y CSS.
Solamente tienen que configurar la ruta raíz de su aplicación web en LogiStudio.Paradise.Minifier.exe.config
y ejecutar el .exe. La aplicación buscará todos los Css y Js y los minificará.

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
  <appSettings>
    <add key="RootFolder" value="C:\Proyectos\LogicStudio\Paradise-v1.1\Paradise\LogicStudio.Paradise\Publish\"/>
  </appSettings>
</configuration>

En value = "Ruta de su aplicacion", configurar la ruta raíz de su aplicación 

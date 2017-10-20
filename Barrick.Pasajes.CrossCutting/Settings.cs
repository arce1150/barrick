using System;
using System.Collections.Generic;
using System.Configuration;

namespace Barrick.Pasajes.CrossCutting
{
   public static class Settings
    {
       public static string CadenaConexion
       {
           get { return ConfigurationManager.ConnectionStrings["conex"].ConnectionString; }
       } 
    }
}

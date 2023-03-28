using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTestApp.Helpers
{
    public static class Constants
    {
        public const string DatabaseFilename = "GameTestSQLite.db3";

        //Configuracion de la base de datos
        public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite | //Permite lectura y escritura
        SQLite.SQLiteOpenFlags.Create | //Crear la base de datos en caso de que no exista
            SQLite.SQLiteOpenFlags.SharedCache; //Habilitar el acceso a la DB multi-hilo

        //Get, regresar la ruta de la BD.
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        //Old Get
        //public static string DatabasePath
        //{
        //    get
        //    {
        //        return Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        //    }
        //}
    }
}

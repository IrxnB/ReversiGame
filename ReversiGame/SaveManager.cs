using Microsoft.Win32;
using ReversiLib;
using ReversiLib.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reversi
{
    internal class SaveManager
    {
        DirectoryInfo _dir;
        FileInfo[] _files;
        private SaveManager()
        {
            _dir = new DirectoryInfo(Properties.Settings.Default.SaveFilesPath);
            _files = _dir.GetFiles();
        }
        public FileInfo[] Files
        {
            get { return _files; }
        }
        public ReversiGame Load(string path)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return new ReversiGame((ReversiGameSerialized)binaryFormatter.Deserialize(stream));
            }
        }
        public void Save(ReversiGame game)
        {
            _files = _dir.GetFiles();
            using (Stream stream = File.Open(Path.Combine(_dir.FullName, DateTime.Now.ToString("HH_mm_ss__dd_MM_yy")), FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, new ReversiGameSerialized(game));
            }
            _files = _dir.GetFiles();
        }
        private static SaveManager _instance;
        public static SaveManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
    }
}

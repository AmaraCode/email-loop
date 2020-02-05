using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;


namespace EmailLoop
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCollection"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public class FileIO<TCollection, TModel> //where TCollection: IList<TModel>
    {

        private string _file;
        private string _path;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath"></param>
        public FileIO(string dataPath)
        {
            _file = typeof(TModel).Name;
            _path = dataPath;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool SaveData(TCollection collection)
        {
            bool status = false;
            try
            {
                if (collection != null)
                {
                    //serialize the collection to JSON
                    //var data = JsonConvert.SerializeObject(collection);

                    string filename = @$"{_path}\{_file}.json";
                    //open file stream
                    using (StreamWriter file = File.CreateText(filename))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, collection);
                    }
                }
                status = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return status;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public TCollection GetData(TCollection collection)
        {
            try
            {
                if (collection != null)
                {
                    //serialize the collection to JSON
                    string filename = @$"{_path}\{_file}.json";

                    using (StreamReader file = new StreamReader(filename))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //collection = serializer.Deserialize<TCollection>()
                        collection = (TCollection)serializer.Deserialize(file, typeof(TCollection));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return collection;
        }
    }
}

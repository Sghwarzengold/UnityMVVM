using System;
using System.Collections.Generic;

namespace UnityMvvm
{
    /// <summary>
    /// Base class for game models. Responsoble for storing GUID for each object
    /// </summary>
    public class Model
    {
        static Dictionary<Guid, Model> guidToModelMap = null;

        public static void Flush()
        {
            if (guidToModelMap == null)
                return;

            foreach (var item in new List<Model>(guidToModelMap.Values))
                item.ModelID = Guid.Empty;
            guidToModelMap.Clear();
        }


        static public Model GetByID(Guid id)
        {
            Model m = null;
            if (!guidToModelMap.TryGetValue(id, out m))
                return null;
            return m;
        }

        private Guid modelId;
        public Guid ModelID
        {
            get { return modelId; }
            set
            {
                guidToModelMap.Remove(modelId);
                modelId = value;

                if (modelId != Guid.Empty && !guidToModelMap.ContainsKey(modelId))
                    guidToModelMap.Add(modelId, this);
            }
        }

        public Model()
        {
            if (guidToModelMap == null)
                guidToModelMap = new Dictionary<Guid, Model>();
            modelId = Guid.NewGuid();
            guidToModelMap.Add(ModelID, this);
        }

        ~Model()
        {
            guidToModelMap.Remove(ModelID);
            ModelID = Guid.Empty;
        }
    }
}
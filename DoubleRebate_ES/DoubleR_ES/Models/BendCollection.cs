using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot.Entities;

namespace DoubleR_ES.Models
{
    public class BendCollection:List<BendData>
    {
        private ReadOnlyCollection<BendData> collection;

        public BendCollection()
        {
            collection = AsReadOnly();
        }

        public BendData this[LineType lineType]
        {
            get
            {
                return collection.FirstOrDefault(data => data.LineType == lineType);
            }
        }

    }
}

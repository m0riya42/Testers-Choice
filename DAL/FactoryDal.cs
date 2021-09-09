using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        static Idal dal = null;
        public static Idal getDal()
        {
            if (dal == null)
                dal = new DALXml();
               // dal = new imp_Dal();
            return dal;
        }
    }
}

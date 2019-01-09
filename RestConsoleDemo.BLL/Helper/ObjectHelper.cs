using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Helper
{
    public static  class ObjectHelper
    {
        public static void CopyValue(object origin, object target)
        {
            System.Reflection.PropertyInfo[] properties = (target.GetType()).GetProperties();
            System.Reflection.PropertyInfo[] fields = (origin.GetType()).GetProperties();
            for (int i = 0; i < fields.Length; i++)
            {
                for (int j = 0; j < properties.Length; j++)
                {
                    if (fields[i].Name == properties[j].Name && properties[j].CanWrite)
                    {
                        properties[j].SetValue(target, fields[i].GetValue(origin), null);
                        break;
                    }
                }
            }
        }

        public static void CopyValueNotKey(object origin, object target,string [] Keys)
        {
            System.Reflection.PropertyInfo[] properties = (target.GetType()).GetProperties();
            System.Reflection.PropertyInfo[] fields = (origin.GetType()).GetProperties();
            for (int i = 0; i < fields.Length; i++)
            {
                bool KeyBj = false;
               
                foreach(string str in Keys )
                {
                    if( fields[i].Name==str)
                    {
                        KeyBj = true;
                        break;
                    }
                }
                if (!KeyBj)
                {
                    for (int j = 0; j < properties.Length; j++)
                    {
                        if (fields[i].Name == properties[j].Name && properties[j].CanWrite)
                        {
                            properties[j].SetValue(target, fields[i].GetValue(origin), null);
                            break;
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Singleton
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static T mInstance;
        private static readonly object mLockedObject = new object();

        protected Singleton()
        {
        }

        public static T Instance
        {
            get
            {
                if(mInstance == null)
                {
                    lock(mLockedObject)
                    {
                        if(mInstance == null)
                        {
                            mInstance = Activator.CreateInstance<T>();
                        }
                    }
                }

                return mInstance;
            }
        }
    }
   
}

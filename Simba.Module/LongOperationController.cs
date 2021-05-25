using System;
using System.Collections.Generic;

namespace CAMS.Module
{
    public class LongOperationTerminateException : Exception
    {
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class BatchCreationOptionsAttribute : Attribute
    {
        private int? objectsCount;
        private int? commitInterval;
        public BatchCreationOptionsAttribute(int objectsCount)
        {
            this.objectsCount = objectsCount;
        }
        public BatchCreationOptionsAttribute(int objectsCount, int commitInterval)
            : this(objectsCount)
        {
            this.commitInterval = commitInterval;
        }
        public int? ObjectsCount
        {
            get
            {
                return objectsCount;
            }
        }
        public int? CommitInterval
        {
            get
            {
                return commitInterval;
            }
        }
    }

    public interface IObjectPropertiesInitializer
    {
        void InitializeObject(int index);
    }
}

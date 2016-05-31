using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos.Demos
{
    public class Properties
    {
        private int _operationsPerformed;

        public Properties(Guid id)
        {
            this.Id = id;
            _operationsPerformed = 0;
        }

        //Events are the one exception where public fields should generally be used
        //The compiler generates efficient and threadsafe code for subscribing/unsubscribing (Since C# 4)
        //See: https://blogs.msdn.microsoft.com/cburrows/2010/03/04/events-get-a-little-overhaul-in-c-4-part-i-locks/
        public event EventHandler OperationPerformed;

        //backed property
        public int OperationsPerformed
        {
            get { return _operationsPerformed; }
            private set
            {
                //Can do validation (or any other operations) in response to set or get
                if (value < _operationsPerformed)
                {
                    throw new ArgumentException("Can only be increased", nameof(this.OperationsPerformed));
                }
                _operationsPerformed = value;
            }
        }

        //Calculated property
        public bool HasOperationBeenPerformed
        {
            get
            {
                return this.OperationsPerformed > 0;
            }
        }

        //Auto property
        public object AutoProperty { get; set; }

        //Auto property with private set - can be read from anywhere but only set from this class
        public object ProtectedAutoProperty { get; private set; }        

        //Read only auto property - can only be set in constructor
        public Guid Id { get; }

        public void PerformOperation()
        {            
            //Update the count of operations
            this.OperationsPerformed += 1;
            //Notify subscribers of operation
            this.OperationPerformed?.Invoke(this, new EventArgs());
        }
    }
}

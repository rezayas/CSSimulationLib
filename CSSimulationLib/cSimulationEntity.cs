using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib
{
    public class SimulationEntity
    {
        protected long _nextID = 0;
        private long _entityID;

        // instantiation
        public SimulationEntity()
        {
            // set entity ID
            _entityID = _nextID;
            ++_nextID;            
        }

        // Properties
        public long ID
        {
            get { return _entityID; }
        }

        // Methods
        public void Reset()
        {
            _nextID = 0;
        }
    }
}

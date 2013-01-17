using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClipingModule
{
    /// <summary>
    /// Enumeration with cliping operation type.
    /// </summary>
    public enum EClipingModuleOperationType
    {
        X1, X2, Y1, Y2, Z1, Z2
    }

    /// <summary>
    /// Arguments of event used during cliping operation.
    /// </summary>
    public class ClipingEventArgs : EventArgs
    {
        /// <summary>
        /// Operation type - dimension and plane number. 
        /// </summary>
        public EClipingModuleOperationType Type { get; set; }

        /// <summary>
        /// New plane position.
        /// </summary>
        public int Position { get; set; }
    }
}

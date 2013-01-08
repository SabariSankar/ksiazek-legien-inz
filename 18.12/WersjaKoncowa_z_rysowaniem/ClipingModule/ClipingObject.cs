using System;
using Kitware.VTK;
using System.Collections.Generic;

namespace ClipingModule
{
    /// <summary>
    /// Class provides support for operation of cliping parts of 3D object.
    /// Cliping planes are parrallel to x, y or z axis. 
    /// </summary>
    public class ClipingObject
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Collection with cutting planes.
        /// </summary>
        private readonly vtkPlaneCollection _planeCollection;

        /// <summary>
        /// Volume size in x dimension.
        /// </summary>
        private int _xSize;
        /// <summary>
        /// Volume size in y dimension.
        /// </summary>
        private int _ySize;
        /// <summary>
        /// Volume size in z dimension.
        /// </summary>
        private int _zSize;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sizeList">List with three elements(x,y,z)</param>
        public ClipingObject(IList<double> sizeList)
        {
            _planeCollection = vtkPlaneCollection.New();

            InitVolumeSizesFromVizulaisation3D(sizeList);

           InitPlaneCollection(_planeCollection);
        }

        /// <summary>
        /// Sets volume size info. 
        /// </summary>
        /// <param name="sizeList">List with three elements(x,y,z)</param>
        private void InitVolumeSizesFromVizulaisation3D(IList<double> sizeList)
        {
            _xSize = (int)sizeList[0];
            _ySize = (int)sizeList[1];
            _zSize = (int)sizeList[2];
        }

        /// <summary>
        /// Sets initial planes' positions.
        /// </summary>
        /// <param name="planeCollection"></param>
        private void InitPlaneCollection(vtkPlaneCollection planeCollection)
        {
            var planeX1 = new vtkPlane();
            planeX1.SetNormal(1, 0, 0);
            planeX1.SetOrigin(0, 0, 0);
            planeCollection.AddItem(planeX1);

            var planeX2 = new vtkPlane();
            planeX2.SetNormal(-1, 0, 0);
            planeX2.SetOrigin(_xSize, 0, 0);
            planeCollection.AddItem(planeX2);

            var planeY1 = new vtkPlane();
            planeY1.SetNormal(0, 1, 0);
            planeY1.SetOrigin(0, 0, 0);
            planeCollection.AddItem(planeY1);

            var planeY2 = new vtkPlane();
            planeY2.SetNormal(0, -1, 0);
            planeY2.SetOrigin(0, _ySize, 0);
            planeCollection.AddItem(planeY2);

            var planeZ1 = new vtkPlane();
            planeZ1.SetNormal(0, 0, 1);
            planeZ1.SetOrigin(0, 0, 0);
            planeCollection.AddItem(planeZ1);

            var planeZ2 = new vtkPlane();
            planeZ2.SetNormal(0, 0, -1);
            planeZ2.SetOrigin(0, 0, _zSize);
            planeCollection.AddItem(planeZ2);
        }

        /// <summary>
        /// Changes cliping plane position.
        /// </summary>
        /// <param name="args">EventArgs with operation type and new plane position</param>
        /// <returns>Modified plane</returns>
        public vtkPlaneCollection Clip(ClipingEventArgs args)
        {
            try
            {
                switch (args.Type)
                {
                    case (EClipingModuleOperationType.X1):
                        _planeCollection.GetItem(0).SetOrigin(args.Position, 0, 0);
                        break;
                    case (EClipingModuleOperationType.X2):
                        _planeCollection.GetItem(1).SetOrigin(_xSize - args.Position, 0, 0);
                        break;
                    case (EClipingModuleOperationType.Y1):
                        _planeCollection.GetItem(2).SetOrigin(0, args.Position, 0);
                        break;
                    case (EClipingModuleOperationType.Y2):
                        _planeCollection.GetItem(3).SetOrigin(0, _ySize - args.Position, 0);
                        break;
                    case (EClipingModuleOperationType.Z1):
                        _planeCollection.GetItem(4).SetOrigin(0, 0, args.Position);
                        break;
                    case (EClipingModuleOperationType.Z2):
                        _planeCollection.GetItem(5).SetOrigin(0, 0, _zSize - args.Position);
                        break;
                }
                
            }catch(Exception e)
            {
                logger.ErrorException("Cliping opeartion exception.", e);
                return null;
            }
            return _planeCollection;
        }

    }

}

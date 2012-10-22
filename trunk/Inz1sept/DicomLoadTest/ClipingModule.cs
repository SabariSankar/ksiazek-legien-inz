using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kitware.VTK;

namespace DicomLoadTest
{
    public class ClipingModule
    {
        private readonly vtkVolumeMapper _mapper;
        private readonly vtkPlaneCollection _planeCollection;
        private readonly Visualization3D _visualization3D;

        private int _xSize;
        private int _ySize;
        private int _zSize;

        private ClipingToolbox _clipingToolbox;

        public ClipingModule(Visualization3D visualization3D)
        {
            _visualization3D = visualization3D;
            _mapper = visualization3D.Mapper;
            _planeCollection = vtkPlaneCollection.New();

            var sizeArray = _visualization3D.GetObjectSize();
            _xSize = (int)sizeArray[0];
            _ySize = (int)sizeArray[1];
            _zSize = (int)sizeArray[2];

           InitPlaneCollection(_planeCollection);
        }

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


        //jesli zwraca true, ToolBox jest widoczny
        public bool ShowToolbox()
        {
            if (_clipingToolbox == null || _clipingToolbox.IsDisposed)
            {
                _clipingToolbox = new ClipingToolbox(_visualization3D.GetObjectSize(),this);
                _clipingToolbox.Visible = true;
                return true;
            }
            else if (_clipingToolbox.Visible)
            {
                _clipingToolbox.Visible = false;
                return false;
            }
            else
            {
                _clipingToolbox.Visible = true;
                return true;

            }
        }

        public void ExecuteClipingOperation(ClipingEventArgs args)
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
                _mapper.SetClippingPlanes(_planeCollection);
                _visualization3D.Update3DVisualization();
            }catch(Exception e)
            {
                //logger
            }
        }

    }

    public class ClipingEventArgs : EventArgs
    {
        public EClipingModuleOperationType Type { get; set; }
        public int Position { get; set; }
    }

    public enum EClipingModuleOperationType
    {
        X1, X2, Y1, Y2, Z1, Z2
    }
}

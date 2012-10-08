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
        private vtkPlaneCollection _planeCollection;

        public ClipingModule(vtkVolumeMapper mapper)
        {
            _mapper = mapper;
            _planeCollection = vtkPlaneCollection.New();
        }

        public void ExecuteClipingOperation(ClipingEventArgs args)
        {
            switch(args.Type)
            {
                case(EClipingModuleOperationType.NewPlane): 
                    AddPlane(args);
                    break;
                case(EClipingModuleOperationType.Change):
                    ChangePlane(args);
                    break;
                case(EClipingModuleOperationType.RemovePlane):
                    RemoveLastPlane();
                    break;
                case(EClipingModuleOperationType.ClearAll):
                    ClearObject();
                    break;
            }
            _mapper.SetClippingPlanes(_planeCollection);
        }

        private void AddPlane(ClipingEventArgs args)
        {
            var plane = vtkPlane.New();
            plane.SetOrigin((double) args.A,(double) args.B, (double) args.C);
            plane.SetNormal((double)args.XNormal, (double)args.YNormal, (double)args.ZNormal);
            _planeCollection.AddItem(plane);
        }

        private void ChangePlane(ClipingEventArgs args)
        {
            var size = _planeCollection.GetNumberOfItems();
            if(size > 0)
            {
                var plane = _planeCollection.GetItem(size - 1);
                plane.SetOrigin((double)args.A, (double)args.B, (double)args.C);
                plane.SetNormal((double)args.XNormal, (double)args.YNormal, (double)args.ZNormal);
            }
        }

        private void RemoveLastPlane()
        {
            var size = _planeCollection.GetNumberOfItems();
            if(size > 0)
                _planeCollection.RemoveItem(_planeCollection.GetItem(size - 1));
        }

        private void ClearObject()
        {
            _planeCollection.RemoveAllItems();
        }

    }

    public class ClipingEventArgs : EventArgs
    {
        public  EClipingModuleOperationType Type { get; set; }

        public decimal A { get; set; }
        public decimal B { get; set; }
        public decimal C { get; set; }

        public decimal XNormal { get; set; }
        public decimal YNormal { get; set; }
        public decimal ZNormal { get; set; }
    }

    public enum EClipingModuleOperationType
    {
        NewPlane,
        Change,
        RemovePlane,
        ClearAll
    }
}

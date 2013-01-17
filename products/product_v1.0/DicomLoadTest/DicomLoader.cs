using System;
using Kitware.VTK;

namespace MainWindow
{

    public class DicomLoader
    {
        private vtkDICOMImageReader _dicomReader;

        public DicomLoader(String directoryPath)
        {
            _dicomReader = new vtkDICOMImageReader();
            _dicomReader.SetDirectoryName(directoryPath);
            _dicomReader.Update();        
        }

        public void ChangeDirectory(String directoryPath)
        {
            _dicomReader.Dispose();
            _dicomReader = new vtkDICOMImageReader();
            _dicomReader.SetDirectoryName(directoryPath);
            _dicomReader.Update();
        }

        public vtkDataSet GetOutput()
        {
            return _dicomReader.GetOutput();
        }

        public int GetErrorCode()
        {
            return (int)_dicomReader.GetErrorCode(); 
        }

        public void Dispose()
        {
            _dicomReader.Dispose();
        }
    }
}

namespace GymWeb.Models.Utils {
    public class FileManagerModels {
        public FileInfo[] Files { get; set; }
        public IFormFile IFormFile { get; set; }
        public List<IFormFile> IFormFiles { get; set; }
        public string PathImage { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace RapidApi.Model
{
    public class Actions
    {
        private readonly Context _context;

        public Actions(Context context)
        {
                _context = context;
        }
        public int DeletePic(int id)
        {

            var control = _context.Pictures.SingleOrDefault(x => x.Id == id);
            if (control == null) { return 0; }

            _context.Pictures.Remove(control);
            _context.SaveChanges();
            return 1;
        }
        public Picture GetSinglePicture(string pictureTitle)
        {
            var singlePicModel = _context.Pictures.FirstOrDefault(x => x.title == pictureTitle);

            if (singlePicModel !=null) { return singlePicModel; }
            return null;
        }

    }
}

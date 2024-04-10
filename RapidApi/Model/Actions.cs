using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.Eventing.Reader;

namespace RapidApi.Model
{
    public class Actions
    {
        
        private readonly Context _context;
        public delegate int actionDelegate(Picture model);
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

        public int DbAdd(Picture model)
        {
            if(model != null) 
            { 
                    if(!_context.Pictures.Where(x=>x.title == model.title).Any())
                    { _context.Pictures.Add(model); _context.SaveChanges(); return 1;}
                    else
                    { return 0; }                     
            }
            return 0;
        }
        public int DbPutUpdate(Picture model)
        {
            var control = _context.Pictures.FirstOrDefault(x => x.Id == model.Id);
            if (control!=null)
            {
                control.description = model.description;
                control.title = model.title;
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int add(Picture model)
        {
            actionDelegate action = DbAdd;
            return action(model);
        }
        public int Put(Picture model) 
        {
            actionDelegate putAction = DbPutUpdate;
            
            return putAction(model);
        }

    }
}

using FacesApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace FacesApp.Services.Media
{
    public class MediaService : IMediaService
    {
        public async Task<Photo> TakePhotoAsync(string name)
        {
            byte[] photo = null;
            await CrossMedia.Current.Initialize();
            if (CrossMedia.Current.IsCameraAvailable
                && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Full,
                    Directory = "People",
                    Name = name + ".jpg",
                    MaxWidthHeight = 512,
                    AllowCropping = true
                });

                if (file != null)
                {
                    using (Stream photoStream = file.GetStream())
                    {
                        photo = new byte[photoStream.Length];
                        await photoStream.ReadAsync(photo, 0, (int)photoStream.Length);
                    }
                }
            }

            return new Photo() { PhotoData = photo, Name = name + ".png" };
        }

        public async Task<Photo> SelectPhotoAsync(string name)
        {
            byte[] photo = null;
            await CrossMedia.Current.Initialize();
            MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Full,
                MaxWidthHeight = 512,
                CustomPhotoSize = 5120,
            });

            if (file != null)
            {
                using (Stream photoStream = file.GetStream())
                {
                    photo = new byte[photoStream.Length];
                    await photoStream.ReadAsync(photo, 0, (int)photoStream.Length);
                }
            }

            return new Photo { PhotoData = photo, Name = name + ".png" };
        }
    }
}
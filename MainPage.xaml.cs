using System.Reflection;

namespace Maui_Issue13858;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void btnCar_Clicked(object sender, EventArgs e) 
    {
        var assembly = this.GetType().GetTypeInfo().Assembly;
        byte[] imgData = ResourceImageToByte(assembly, "Maui_Issue13858.Resources.Images.img1.png");
        imgTest.Source = ByteCacheToImageSource(imgData);
    }

    private void btnBicycle_Clicked(object sender, EventArgs e) 
    {
        var assembly = this.GetType().GetTypeInfo().Assembly;
        byte[] imgData = ResourceImageToByte(assembly, "Maui_Issue13858.Resources.Images.img2.png");
        imgTest.Source = ByteCacheToImageSource(imgData);
    }

    public static byte[] ResourceImageToByte(Assembly assembly, string resouceId) 
    {
        byte[] data;
        using (Stream s = assembly.GetManifestResourceStream(resouceId)) 
        {
            long length = s.Length;
            data = new byte[length];
            s.Read(data, 0, (int)length);
        }
        return data;
    }

    public static ImageSource ByteCacheToImageSource(byte[] data, string cacheName = "cache") 
    {
        ImageSource imageSource;
        string cachePath = System.IO.Path.Combine(FileSystem.CacheDirectory, cacheName);
        if (File.Exists(cachePath)) 
        {
            File.Delete(cachePath);
        }
        File.WriteAllBytes(cachePath, data);
        imageSource = ImageSource.FromFile(cachePath);

        return imageSource;
    }
}


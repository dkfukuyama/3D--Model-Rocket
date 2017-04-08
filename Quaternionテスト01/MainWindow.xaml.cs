using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Quaternionテスト01
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            // trackball
            var td = new _3DTools.TrackballDecorator();
            root.Children.Add(td);

            // viewport
            var viewport = new Viewport3D();
            td.Content = viewport;

            // camera
            var camera = new PerspectiveCamera();
            camera.Position = new Point3D(14, 13, 12);
            camera.LookDirection = new Vector3D(-14, -13, -12);
            camera.UpDirection = new Vector3D(0, 1, 0);
            viewport.Camera = camera;

            // light
            var light = new DirectionalLight();
            light.Color = Colors.White;
            light.Direction = new Vector3D(-2, -3, -1);
            var lightModel = new ModelVisual3D();
            lightModel.Content = light;
            viewport.Children.Add(lightModel);

            var light2 = new AmbientLight();
            light2.Color = Color.FromRgb(128, 128, 128);
            var lightModel2 = new ModelVisual3D();
            lightModel2.Content = light2;
            viewport.Children.Add(lightModel2);

            // axis
            var xAxis = new _3DTools.ScreenSpaceLines3D();
            xAxis.Points.Add(new Point3D(-100, 0, 0));
            xAxis.Points.Add(new Point3D(100, 0, 0));
            xAxis.Color = Colors.Red;
            xAxis.Thickness = 1;
            var yAxis = new _3DTools.ScreenSpaceLines3D();
            yAxis.Points.Add(new Point3D(0, -100, 0));
            yAxis.Points.Add(new Point3D(0, 100, 0));
            yAxis.Color = Colors.Green;
            yAxis.Thickness = 1;
            var zAxis = new _3DTools.ScreenSpaceLines3D();
            zAxis.Points.Add(new Point3D(0, 0, -100));
            zAxis.Points.Add(new Point3D(0, 0, 100));
            zAxis.Color = Colors.Blue;
            zAxis.Thickness = 1;
            var axis = new ModelVisual3D();
            axis.Children.Add(xAxis);
            axis.Children.Add(yAxis);
            axis.Children.Add(zAxis);
            viewport.Children.Add(axis);

            // sphere
            var sphere = new Sphere3D();
            var trans = new Transform3DGroup();
            trans.Children.Add(new ScaleTransform3D(2.5, 2.5, 2.5));
            trans.Children.Add(new TranslateTransform3D(2.5, 2.5, 2.5));
            sphere.Transform = trans;
            viewport.Children.Add(sphere);
        }
    }
}
        }
    }
}

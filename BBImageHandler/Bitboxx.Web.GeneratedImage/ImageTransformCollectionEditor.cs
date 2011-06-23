using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using Bitboxx.Web.GeneratedImage.Transform;

namespace Bitboxx.Web.GeneratedImage
{
	public class ImageTransformCollectionEditor : CollectionEditor {
		private static readonly Type[] s_newItemTypes = new Type[] {
		                                                           	typeof(ImageResizeTransform)
		                                                           };

		public ImageTransformCollectionEditor(Type type)
			: base(type) {
			}

		protected override Type[] CreateNewItemTypes() {
			return s_newItemTypes;
		}

		protected override Type CreateCollectionItemType() {
			return typeof(ImageTransform);
		}

		private sealed class ImageTransform : Transform.ImageTransform {

			public override Image ProcessImage(Image image) {
				throw new NotImplementedException();
			}

			public int DummyProperty {
				get {
					Debug.Fail("Should not be called");
					return 1;
				}
				set {
					Debug.Fail("Should not be called");
				}
			}
		}
	}
}
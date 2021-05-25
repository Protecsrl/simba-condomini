using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.IO;
namespace CAMS.Module.DBAux
{
    [DefaultClassOptions]
    [Persistent("FILEDATAEMAIL")]
    [System.ComponentModel.DefaultProperty("FileName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "File Data Mail")]
    [ImageName("BO_Employee")]
   // [NavigationItem(true)]
   // [VisibleInDashboards(false)]
    public class FileDataMail : BaseObject, IFileData, IEmptyCheckable
	{
		private string fileName = "";
#if MediumTrust
		private int size;
		public int Size {
			get { return size; }
			set { SetPropertyValue("Size", ref size, value); }
		}
#else
		[Persistent]
		private int size;
		public int Size
		{
			get { return size; }
		}
#endif
		public FileDataMail(Session session) : base(session) { }
		public virtual void LoadFromStream(string fileName, Stream stream)
		{
			Guard.ArgumentNotNull(stream, "stream");
			FileName = fileName;
			byte[] bytes = new byte[stream.Length];
			stream.Read(bytes, 0, bytes.Length);
			Content = bytes;
		}
		public virtual void SaveToStream(Stream stream)
		{
			if (Content != null)
			{
				stream.Write(Content, 0, Size);
			}
			stream.Flush();
		}
		public void Clear()
		{
			Content = null;
			FileName = String.Empty;
		}
		public override string ToString()
		{
			return FileName;
		}
		[Size(260)]
		public string FileName
		{
			get { return fileName; }
			set { SetPropertyValue(nameof(FileName), ref fileName, value); }
		}
		[Persistent, Delayed(true)]
		[ValueConverter(typeof(CompressionConverter))]
		[MemberDesignTimeVisibility(false)]
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public byte[] Content
		{
			get { return GetDelayedPropertyValue<byte[]>(nameof(Content)); }
			set
			{
				int oldSize = size;
				if (value != null)
				{
					size = value.Length;
				}
				else
				{
					size = 0;
				}
				SetDelayedPropertyValue(nameof(Content), value);
				OnChanged(nameof(Size), oldSize, size);
			}
		}
		#region IEmptyCheckable Members
		[NonPersistent, MemberDesignTimeVisibility(false)]
		public bool IsEmpty
		{
			get { return string.IsNullOrEmpty(FileName); }
		}
		#endregion

	}
}

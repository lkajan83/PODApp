namespace PODApp.ViewModels
{
	public interface IDraggableListAdapter
	{

		int mMobileCellPosition { get; set; }

		void SwapItems (int from, int to);
	}
}


   M S B A M L   `   `   `  ���� F  BVideoPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:VideoPlayerVideoPlayer      VideoPlayer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:VideoPlayer     -     � Title$  
MainWindow��$	��450��$	��800��.��� 
���   �.+�� $��0��$��0������ *	RowSpan+���� *
ColumnSpan+.+�r� -    r�Name$ MediaElement�� r� ScrubbingEnabled . �� $��50��$��Right=�
������� J� Opacity �� *VisOpa+.+��� ���� *Files+�� -    ��Name$ pParentPanel�� �� Orientation$ Vertical=�$��Bottom=�$��Stretch=� �� *VisOpa+.+��  � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+��� -    ��Name$ Timenow��$��0��
����������� *Value+ �� ElementName$ Slider���� -   	 ��Name$	 Slider��$��1��
 � Minimum$
 0Q� � Value$ 0Q� �� TickFrequency$ 1Q��� -   $ 	togoLabel��$��2��
��������� $ 
Horizontal=�.+��� -   $��Start���� -   $
��Stop���� -	   $��Previous���� -
   $
��Next���� -   $��
FullScreen������ *FullScreenCommand+
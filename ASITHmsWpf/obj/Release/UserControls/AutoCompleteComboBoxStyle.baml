   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   NormalBrush  �� 
StartPoint �� EndPoint � Color � Offset  HorizontalNormalBrush  
LightBrush  HorizontalLightBrush  	DarkBrush  PressedBrush  DisabledForegroundBrush �� Color  DisabledBackgroundBrush  WindowBackgroundBrush 	 SelectedBackgroundBrush 
 NormalBorderBrush  HorizontalNormalBorderBrush  DefaultedBorderBrush  PressedBorderBrush  DisabledBorderBrush  SolidBorderBrush  LightBorderBrush  
GlyphBrush  LightColorBrush  ComboBoxToggleButton �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius	 �� Triggers
 P� Property P� Value �� 
TargetName �� Value �� Property  ComboBoxTextBox1    +ASITHmsWpf.UserControls.AutoCompleteTextBox �� 
TargetType �� 	ClickMode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation P� 
SourceName �� Triggers�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     /local%clr-namespace:ASITHmsWpf.UserControls  :x,http://schemas.microsoft.com/winfx/2006/xaml     %  &	        &	 <     &	 �     &	 �     &	 >    &	 z    &	     &	     &	 $    &		 4    &	
 D    &	 �    &	 �    &	 F    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    7   7 
 7  7  7  7  &	 �    '   0    7  7  7  7  7  �� $	  0,0&�$	 0,1&���� � $
 #FFF��$	 0.0Q��� $	  0,0&�$	 1,0&���� � $
 #FFF��$	 0.0Q�� $
 #CCC��$	 1.0Q��� $	  0,0&�$	 0,1&���� � $
 #FFF��$	 0.0Q�� $
 #EEE��$	 1.0Q��� $	  0,0&�$	 1,0&���� � $
 #FFF��$	 0.0Q�� $
 #EEE��$	 1.0Q��� $	  0,0&�$	 0,1&���� � $
 #FFF��$	 0.0Q��� $	  0,0&�$	 0,1&���� � $
 #BBB��$	 0.0Q�� $
 #EEE��$	 0.1Q�� $
 #EEE��$	 0.9Q�� $
 #FFF��$	 1.0Q��� $
 #888���� $
 #EEE���� $
 #FFF���� $
 #DDD���� $	  0,0&�$	 0,1&���� � $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	  0,0&�$	 1,0&���� � $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	  0,0&�$	 0,1&���� � $
 #777��$	 0.0Q�� $
 #000��$	 1.0Q��� $	  0,0&�$	 0,1&���� � $
 #444��$	 0.0Q�� $
 #888��$	 1.0Q��� $
 #AAA���� $
 #888���� $
 #AAA���� $
 #444���� $
 #DDD���� " d�.R��  �� �� $��20 �.+��� $ Border��$��2��$ 0��8��  8�� $��1q�	 P� 
 �@J�IsMouseOver . . ��� $ Border��8   � ��P� 
 � {� .  . ��� $ Border��8   � ���� $ Border��8   � ���� 8   � ���� " ��.R��� $ PART_ContentHost��|�.  #��z���� "   .���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��$	 120����  � ��$ 20����  �@��
IsEditable . ��  � �� �� "   .R�� .+�d� $��ToggleButton��8��  $��2��|�.  $ Press=��� $��ContentSite�� .  #��z #��z #��z��$��3,3,23,3q�$��Center=�$
��Left=��� $ PART_EditableTextBox��8�� $
��Left=�$��Center=�$��3,3,23,3q�|�. 
������ $y�Hidden=�# z ��Z� � $��Popup��$��Bottom=�#��z  . |�.  $��Slide=�.��� $��DropDown�� . #��z #��z .+��� $ DropDownBorder��8�� $��1q�8�� �� $��4,6,4,6q� . .����  . $ 	Contained=�	 P� 
 �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P� 
 � {� .  . ��� 8   � ��P� 
 �@��
IsGrouping . . ���  � �� .  P� $ Popup��
 �@�AllowsTransparency . . ��� $ DropDownBorder�� �@��CornerRadius$ 2���� $ DropDownBorder�� � ��$ 0,2,0,0q�P� 
 �@��
IsEditable . . ���  � �� .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=� 
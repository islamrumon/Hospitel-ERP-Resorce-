   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  +    %ASITHmsWpf.Accounting.frmEntryAccMgt1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  :d2http://schemas.microsoft.com/expression/blend/2008  -   
  � Tag$%  Accounts Management Admin Panel��   IOUserControl#��[   � 	Resources   RowColor  RowHoverColor  RowSelectedColor  RowHeaderColor  RowHeaderSelectedColor  ColumnHeaderHoverColor  SelectedForegroungColor  HeaderTextColor �� Color 	 HeaderBackgroundColor 
 DataGridBackgroundColor  RowBorderColor  RowSelectedTextColor  IDColumnText  CellBorderColor=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell �� 
TargetType ��Name J� SnapsToDevicePixels	 �� Triggers
 P� Property P� Value �� 
TargetName��%  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �     &	
 �     &	 �     &	 �     &	 �     &	     '      '  =    7  ��	#FFECECEC����	#FFFBFBFB����	#FFFBFBFB����	#FF1885A1����	#FF12B0DA����	#FF12B0DA����	#FF1885A1���� $ 	#FFFFFFFF���� $ 	#FF1885A1���� $ 	#FFECECEC���� $ 	#FFD8D8D8���� $ 	#FF1885A1���� $ 	#FF808080���� $ 	#FFD8D8D8���� "  .���  � ��$
 Bold"��� "  .���  �@�FocusVisualStyle Z� ��  � �� �� "  .R��� $ border��
������ $��1q�
������  . $	��2,2q�._��� $��CellContent�� . 	 P� 
 �@ 
IsSelected . . ��� 8    � ��P� 
 �@J�IsKeyboardFocusWithin . . ��� $ border�� � ��
 ��  �.��� .+�]� $��
0,50, 0, 0q�.+��� -    ��Name$ 	tabPanel1��$	��Top=�$��Stretch=�$��Stretch=� �� TabStripPlacement$	 Top=�.@��� $��_1. Voucher Approval��$	��150��$��25��  StdTabItemStyle01#��[ .��� ���	#FFE4F2E1 � ColumnDefinitions �� $��0* ��� .+��� $��Cheque Delivery��$
��Left=�$��373,120,0,0q�$	��Top=�$��2���� $��3��{�.  �� $��_2. Voucher Editing��$	��150��$��25��#��[ .��� ���	#FFE4F2E1.+��� $��Cheque Print��$
��Left=�$��373,120,0,0q�$	��Top=��� $��3��{�.  �� $��_3. Cheque Book��$	��150��$��25��#��[ .��� ���	#FFE4F2E1.+���  �� Orientation$ Vertical=�$��10,10,10,10q�.+��� $ 
Horizontal=�$	��Top=�.+��� -    ��Name$ lblChqBankTitle��$��_Bank Name:��$��Right=�$	��120��$��14(�$
��Bold"� �� Target ��  �� ElementName$ 
cmbChqBank���� -    ��Name$ 
cmbChqBank��
�������$	��500�� �� 
IsReadOnly .  �� SelectedIndex$ 0��$��14(�$
��Bold"��� -    ��Name$ btnBookNext��  StdBtnStyle01#��[ $
��Next��$��60��$��40,0,0,0q�$y�Hidden=��� -   $ btnExistBook��#��[ $��Show Existing Books��$	��120��$��40,0,0,0q��� -   $ 
btnRegBook��#��[ $��Add New Book��$	��120��$��13,0,0,0q��  � RowDefinitions �� �� .+��� -    ��Name$ stkpBookList��$ 
Horizontal=�$��0,10,0,0q�$��0��$y�Hidden=�$��32��.+��� -	   $ lblExistChqBookTitle��$��Existing _Books:��$��Right=�$	��120��$��14(�$
��Bold"� �� $ cmbExistChqBook���� -
   $ cmbExistChqBook��
�������$	��760�� . $ 0��$��14(�$
��Bold"��� -   $ btnShowLeafs��#��[ $��Show Status��$	��120��$��13,0,0,0q��� -   $ stkpNewBookReg��$ Vertical=�$��0,20,0,0q�$��0��$y�Visible=�.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Registration _Date : ��|�.  $��Right=�$��Center=�$	��120��$��	120,0,0,0q�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpRegDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��24��$	��130��{��� $ chkDateBlocked�� �� Path$ 	IsChecked��� -   $ lblLeafStartTitle��$��Leaf Start No.:��$��Right=�$	��100��$��26�� �� $ txtLeafStart��(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtLeafStart�� �� TextAlignment$ Right=�$��Center=�)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .     AutoSelectBehavior$  OnFocus=� 
 wtxtBx#��[ !   	Watermark$!  ��$�� ��$
��Left=�$��24��$	��150���� -   $ lblLeafQtyTitle��$��Leaf Quantity:��$��Right=�$	��100��$��26�� �� $ 
txtLeafQty���� -   $ 
cmbLeafQty��$��50��.@��� $��10��$  10���� $��20��$  20���� $��25��$  25���� $��50��$  50��" �� 
IsSelected" . �� $	��100��$	  100���� -   $ 
btnGenLeaf��#��[ $��Show New Leaves��$	��120��$��13,0,0,0q��� -   $ btnUpdateNewLeaf��#��[ $��Save New Book��$	��120��$��13,0,0,0q��� $ 
Horizontal=�$��0,10,0,0q�.+��� -   $ 
chkChqRmrk��$��Rrmarks (if any):��$��Center=�$	��120��|�.  $��	120,0,0,0q�  -   $ 
txtChqRmrk��$
 Left=�$��Center=� . $  OnFocus=�$��26��$��5,0,0,0q�#��[ $!  ��$�� ��$
��Left=�$	��600��|��� $ 
chkChqRmrk��$ 	IsChecked��� -   $ stkpBookStatus��$ Vertical=�$��1��.+�&   System.Windows.Controls.DataGrid  -   #  Name$# 
dgChqLeaf1��$   CanUserResizeRows$ .  
������ %   
IsReadOnly% .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��250��$
��Auto=�&   AutoGenerateColumns& .  '   CanUserAddRows' .  (   CanUserDeleteRows( .  $��Hidden=�)   SelectionUnit$
) Cell=�*   SelectionMode$* Single=�+   AlternatingRowBackground+ �	#FFE4F2E1,   Columns, 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn-   Width5	  /System.Windows.Controls.DataGridLengthConverter$- 35	 .   Header$	. Sl#��/   
IsReadOnly/ . 0   CellTemplate0 �� .R�� $��Right=�$	��Top=�$��30��.+��� $��Right=�$	��Top=����� *slnum+1 �� StringFormat$1 
{0: ##0) }��0
  *System.Windows.Controls.DataGridTextColumn
  $- 80	 $. 
Cheque No.��2   CanUserSort2 .  / . 1  +System.Windows.Controls.DataGridBoundColumn3   Binding3 �� $ cheqnum�
  $	- 200	 $. Voucher No.��4 +� IsFocusScope4 .  / . 2 .  5   CanUserReorder5 .  6   CanUserResize6 .  3 �� $ vounum1�
  $	- 100	 $. Cheque Date��4 .  2 .  / . 5 .  6 .  3 �� $ cheqdat1�  $	- 120	 $. Cheque Amount��/ . 0 �� .R�  -   $ Right=� . $��0q�$	��110��$  OnFocus=�$��5,0,0,0q�#��[ $! Amount��7 �� 
IsReadOnly7 . ���� $ cheqam�8 �� Mode$8 TwoWay=�9 �� UpdateSourceTrigger$9 PropertyChanged=�$ 1 {0: #,##0.00;(#,##0.00); }��
  $	- 100	 $. Delivery Date��4 .  2 .  5 .  6 .  3 �� $ 	delivdat1�
  $- 45	 $. Status��4 .  2 .  5 .  6 .  3 �� $ 
cheqstatus�
  $	- 300	 $. Remarks��4 .  2 .  5 .  6 .  3 �� $ 
statusnote�
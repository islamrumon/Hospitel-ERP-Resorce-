   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ASITHmsWpf
ASITHmsWpf  (    "ASITHmsWpf.General.frmSirCodeBook1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  $localclr-namespace:ASITHmsWpf  -   
  � Tag$4  .Subsidery/Resource Code Book Input/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $��35 ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��480 ��� $
��Auto �.+��� -    ��Name$ stkp1��$��0��$��0��$��2��$
��Left=�$	��Top=�$��20,0,0,0q�.+��� $��0,0,0,0q���  �� Orientation$ 
Horizontal=�$
��Left=�.+��� $	��190��$
��Left=�$��20(�Resource Code Book�� -    ��Name$ btnExpandAll��$��_+�� � ToolTip$ Click to expand all��$��16��$��20��$��10(�
������ �� $��Button��$��20��$��10��|�.  $y�Hidden=��� -   $ btnCloseAll��$��_-��$ Click to collapse all��$��16��$��20��$��10(�
������ �� $��10��$y�Hidden=��  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ 
txtBxClSrc�� �� TextWrapping$
 Wrap=�	   	Watermark$	 Search Item��$��2q�$	��150��$��23��$��0��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox
   AutoSelectBehavior$
 OnFocus=��� -   $ btnClr��$y�	Collapsed=�  ButtonImgCl#��[  � Cursor$
 Hand��|�.  $��25��$��23��$��Right=�$��0���� -   $ btnSrc��$y�Visible=�  ButtonImgSc#��[ $
 Hand��|�.  $��25��$��23��$��Right=�$��0���� $��Button��$��20��$��10��$y�Hidden=��� -    ��Name$ CkBxSrc��$��13(�$	��All��$��Center=�|�.  �� -	   $ chkExtraInfo��$��
Extra Info��$��13(�$��	100,5,0,0q�$	��110���  � AllowsTransparency . $��Slide=� � HorizontalOffset$ 0�� � VerticalOffset$ 30������  �� ElementName$ chkExtraInfo�� �� Path$ 	IsChecked� � PlacementTarget �� $ chkAdEd��.���� $��0,0,5,5q�$��0q� �� CornerRadius$ 8������  �� EndPoint$	 0,1&� �� 
StartPoint$	 0,1&�.���  � Color$ 	#FFECFBFB�� � Offset$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   ShadowDepth$ 5Q�   
BlurRadius$ 20Q�   Opacity$	 0.8Q�   RenderingBias$ Quality=�._��� -
    ��Name$ CanExtraInfo��$	��390��$	��500������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4F2E1��$ 0.491Q�� $ 	#FFE4F2E1��$ 0.750Q�� $ 	#FF69B28B��$ 0.957Q�.+��� -   $ stkExtraInfo��$	��390��$	��500��{�. $��0,0,0,0q�.+��� $��Extra Information��$��Center=�$
��Bold"�$��26�����	#FFEAECCC$��14(��� �� -    ��Name$ txtblCodeExtra��$��12(�$��Code and Description��$
��Bold"�$
��Wrap=�$��5,0,0,0q�$	��385��$��30���� $��5���� -     ��Name$  lvExtraInfo��$	��380��$��12(�
������ $��0q�$
��Auto=�$��Hidden=�! � 	Resources! " �� 
TargetType# �� Property$ �� Value��%4   '��       �� "" ��.��� # � y�$$ 	Collapsed=����� "" ��.��� # � ��$ �	#FFE4F2E1% �� View% �� .P��� & �� Width$& 40��' �� CellTemplate' 5  -System.Windows.Controls.ItemContainerTemplate�  .R��� $	��Top=����� *jobsl+�� $	& 535��'   .R�� $��Center=�$	��Top=�$	��525��.+��� $ Vertical=�.+�   
 wtxtBx#��[ $	 Name of Company��$
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��22��$	��520������ *jobcom+�� $ 
Horizontal=�.+�  #��[ $	 Designation��$
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��22��$	��205������ *jobdsg+�� $��From :��$��Right=�$��45��$��Normal$�&   Xceed.Wpf.Toolkit.DateTimePicker    StdDateTimePickerStyle01#��[ $��22��$	��120���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]](   Value( �� *sdate+�� $
��To :��$��Right=�$��30��$��Normal$�  #��[ $��22��$	��120��( �� *edate+  #��[ $	 Major Duties��) J� 	AllowDrop) . * �� AcceptsReturn* . $
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��45��$	��520������ *	jobdesc+�� -   $ btnUpdateExtra��$��Update Info��$��25��$	��120��$��14(�$��Verdana)�  StdBtnStyle01#��[ �� -   $ chkAdEd��$��	Add/_Edit��$��13(�$��	100,5,0,0q�$	��110���  . $��Slide=�$ 0��$ 30������ $ chkAdEd��$ 	IsChecked� �� $ chkAdEd��.���� $��0,0,5,5q�$��0q�$ 8������ $	 0,1&�$	 0,1&�.��� $ 	#FFECFBFB��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q�   $ Black��$ 5Q�$ 20Q�$	 0.8Q�$ Quality=�._��� $	��390��$	��420������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4F2E1��$ 0.491Q�� $ 	#FF69B28B��$ 0.957Q�.+��� $	��390��$	��420��.+��� $ 
Horizontal=�$��0,10,0,0q�.+��� -   $ 	txtblMnGr��$��12(�$��Group Description��$
��Bold"�$
��Wrap=�$��5,0,0,0q�$	��385��$��70���� �� -   $ spnlCodeEntry��{�.  $ Vertical=�$
��Left=�$��20,5,0,0q�.+��� $��Resource Code :��$	��350���� $ 
Horizontal=�.+�  -   $ txtSirCode1��$��5,0,0,0q�$��20��+ �� 	MaxLength$+ 2��$
 Wrap=�$��28��,   AutoMoveFocus, . $
 OnFocus=��� $��-��  -   $ txtSirCode2��$��5,0,0,0q�$��20��$+ 2��$
 Wrap=�$��28��, . $
 OnFocus=��� $��-��  -   $ txtSirCode3��$��5,0,0,0q�$��20��$+ 3��$
 Wrap=�$��35��, . $
 OnFocus=��� $��-��  -   $ txtSirCode4��$��5,0,0,0q�$��20��$+ 2��$
 Wrap=�$��28��, . $
 OnFocus=��� $��-��  -   $ txtSirCode5��$��5,0,0,0q�$��20��$+ 3��$
 Wrap=�$��35��$
 OnFocus=��� -   - ��Name$- 
lblSirCode��$��(XX-XX-XXX-XX-XXX)�����	#FF987B7B�� $��5���� $��Description :��$	��350���  �� $��20 �.+�  -   $ 
txtsirdesc��#��[ $	 Description��$��5,0,0,0q�$
 Wrap=�$	��350��$
 OnFocus=��� -   $ btnImgV��$��0��$��20��$��x��$��Right=�|�.  $y�	Collapsed=��� $��5���� $��Unit :��$	��350���  �� $��20 �.+�  -   $ 
txtsirunit��#��[ $	 Entry Unit (if any)��$��5,0,0,0q�$��20��$
 Wrap=�$	��350��$
 OnFocus=��� -   $ btnImgV1��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $��5���� $ 
Horizontal=�.+��� $��Type / Product Code : ��$	��150���  �� .+�  -   $ 
txtsirtype��#��[ $	 Type (if any)��$��5,0,0,0q�$��20��$
 Wrap=�$	��350��$
 OnFocus=��� -   $ btnImgV2��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $��5���� $!��Type Description (if any) :��$	��350���  �� $	��18* ��� $
��157* � �� .+�  -   $ 
txtsirtdes��#��[ $��0��$	 Type Description (if any)��$��5,0,0,0q�$��20��$
 Wrap=�$	��350��, . $
 OnFocus=�$��2���� -   $ btnImgV3��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=�$��1���� $��10���� $ 
Horizontal=�$��20,0,0,0q�.+��� $��50��$y�Hidden=��� -    $ btnAdd��$	��Add��$��25��$��60��$��14(�$��Verdana)�#��[ �� $��12��$y�Hidden=�|�.  �� -!   $ btnEdit��$
��Edit��$��25��$��60��$��14(�$��Verdana)�#��[ �� $��12��$y�Hidden=�|�.  �� -"   $ 	btnUpdate��$
��Save��$��25��$��60��$��14(�{�.  $
  None��$��Verdana)�#��[ �� $��12��$y�Hidden=�|�.  �� -#   $ 	btnCancel��$��Cancel��$��25��$��60��$��14(�{�. $��Verdana)�#��[ �� -$   $ chkPrint��$��_Print Code Book��$��14(�$��	190,5,0,0q��  . $��Slide=����� $ chkPrint��$ 	IsChecked� �� $ chkPrint��.���� $��0,0,5,5q�$��0q�$ 8������ $	 0,1&�$	 0,1&�.��� $ 	#FFECFBFB��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q�   $ Black��$ 5Q�$ 20Q�$	 0.8Q�$ Quality=�._��� $	��134��$	��146������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4F2E1��$ 0.491Q�� $ 	#FF69B28B��$ 0.957Q�z��� -%   theTransform��.+��� -&   $ 	btnPrint1��$��Print��$��20��$��60��$��12(�$��Verdana)�#��[ . �� Left$. 38��
/ �� Top$	/ 116���� �� $��0,0,0,0q�$��5��$y�Hidden=��� $ 
Horizontal=�$��0��$��1��$��2��.+��  �� $	��450 ��� $
��Auto �.+��� $ Vertical=�$
��Left=�$��0��$��15,0,0,0q�$	��430��$��2��$��2��.+��� -'   $ 	spnlLevel��$ 
Horizontal=�.+��� $��M_ain Groups : ��$��12(�$
��Left=�0 �� Target0 �� $ cmbMainGroup1���� $��Level:��$��50��$
��Bold"�$��Center=��� -(   $ 	chkLevel1��$
��Main��$��50��1 d� 	IsChecked$
1 TrueY�{�.  $��Center=��� -)   $ 	chkLevel2��$��Sub-1��$��55��$
1 TrueY�{�.  $��Center=��� -*   $ 	chkLevel3��$��Sub-2��$��55��$
1 TrueY�$��Center=��� -+   $ 	chkLevel4��$��Sub-3��$��55��$
1 TrueY�$��Center=��� -,   $ 	chkLevel5��$��Details��$��60��$
1 TrueY�$��Center=��� --   2 ��Name$2 cmbMainGroup1��$��20��$
��Left=�3 �� SelectedIndex$3 0��4 �� DisplayMemberPath$4 sirtype��5 �� SelectedValuePath$5 sircode��$	��370��$��10(����� *ListSirInfMain1CodeBook+ �� $ sirtdes����� "" ��.��� # �@�ToolTip$ �� $��3q����� $ sirdesc1��� $ 
Horizontal=�$
��Left=�.+��� -.   $2 cmbMainGroup��$��20��$3 0��$4 sirtdes��$5 sircode��$	��370��$��10(����� *ListSirInfMain2CodeBook+ �� $ sirtdes����� "" ��.��� # �@�ToolTip$ �� $��3q����� $ sirdesc1��� $��5��$y�Hidden=��� -/   $ btnShowList��#��[ $��
0, -20,0,0q�$��25��$��_Show��$��50��$��12(�R� -0   6 R�Name$6 treeSir��  treeAccStyle#��[ $	��450��$	��400��$
��Left=�|�.  $��18,0,0,0q��� -1   $ stklstv��$��0��$��1��$	��550��$��Right=�$	��720��.+�   ASITHmsWpf.SirListView  -2   7  Name$7 lvSir��   ASITHmsWpf.SirListView8   MouseDoubleClick$8 lvSir_MouseDoubleClick��9   KeyUp$9 lvSir_KeyUp��$��14(�$	��550��$
��Auto=�$��Hidden=����� "" ��.��� # � ��$ �	#FFE4F2E1�� # � ��$$ 20���� # � ��$$ 12(�% �� .P��� $& 45��$	��Sl#��'   .R��� $��30��$��0,-5,0,0q�$��Right=����� $ slnum��� $	& 120��$��	Item Code��: �� DisplayMemberBinding: �� $ sircode1��� $	& 290��: �� $ sirdesc����� $��5,0,0,0q�$
��Left=�$��Description���� $& 70��$
��Unit��: �� $ sirunit��� $	& 150��$��Type/Product Code��: �� $ sirtype�
   M S B A M L   `   `   `  ���� *  &System.Windows.Controls.Layout.ToolkitqUclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Layout.ToolkitSystem.Windows.Controls  I EASITHmsViewMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW<clr-namespace:ASITHmsViewMan.General;assembly=ASITHmsViewManASITHmsViewMan.General E AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ASITHmsWpf
ASITHmsWpf (   "ASITHmsWpf.General.frmAccCodeBook1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     itoolkitUclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Layout.Toolkit     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     PGeneral<clr-namespace:ASITHmsViewMan.General;assembly=ASITHmsViewMan     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  Y	 UXceed.Wpf.DataGrid, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45xcdg*http://schemas.xceed.com/wpf/xaml/datagrid 	 $localclr-namespace:ASITHmsWpf 	 -   
  � Tag$*  $Accounts Code Book Input/Edit Screen��   IOUserControl#��[  .���  � 	Resources ��%     � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��480 ��� $
��Auto �.+��� -    ��Name$ stkp1��$��0��$��0��$��2��$
��Left=�$	��Top=�$��30��$��30,0,0,0q�.+��� $��0,0,0,0q���  �� Orientation$ 
Horizontal=�$��25��.+��� $	��180��$
��Left=�$��20(��� $�Chart of Accounts���� -    ��Name$ btnExpandAll��|�.  $��_+�� � ToolTip$ Click to expand all��$��16��$��20��$��10(�
������  � Cursor$
 Hand���� $��Button��$��20��$��10��|�.  $y�Hidden=��� -   $ btnCloseAll��$��_-��|�.  $ Click to collapse all��$��16��$��20��$��10(�
������ $
 Hand���� $��Button��$��10��|�.  $��10��$y�Hidden=��  �� $
��24.8 �.+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -   	  Name$	 
txtBxClSrc��
 �� TextWrapping$

 Wrap=�   	Watermark$ Search Item��$��1q�$	��150��$��23��$��0��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoSelectBehavior$ OnFocus=��� -   $ btnClr��$y�	Collapsed=�  ButtonImgCl#��[ $
 Hand��|�.  $��25��$��23��$��Right=�$��0���� -   $ btnSrc��$y�Visible=�  ButtonImgSc#��[ $
 Hand��|�.  $��25��$��23��$��Right=�$��0���� $��Button��$��20��$��10��$y�Hidden=��� -    ��Name$ CkBxSrc��$��13(�$	��All��$��Center=�|�.  �� -	   $ chkExtraInfo��$��
Extra Info��$��13(�$��	100,5,0,0q�$	��110���  � AllowsTransparency . $��Slide=� � HorizontalOffset$ 0�� � VerticalOffset$ 30������  �� ElementName$ chkExtraInfo�� �� Path$ 	IsChecked� � PlacementTarget �� $ chkAdEd��.���� $��0,0,5,5q�$��0q� �� CornerRadius$ 8������  �� EndPoint$	 0,1&� �� 
StartPoint$	 0,1&�.���  � Color$ 	#FFECFBFB�� � Offset$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   ShadowDepth$ 5Q�   
BlurRadius$ 20Q�   Opacity$	 0.8Q�   RenderingBias$ Quality=�._��� -
    ��Name$ CanExtraInfo��$	��390��$	��500������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4F2E1��$ 0.491Q�� $ 	#FFE4F2E1��$ 0.750Q�� $ 	#FF69B28B��$ 0.957Q�.+��� -   $ stkExtraInfo��$	��390��$	��500��{�. $��0,0,0,0q�.+��� $��Extra Information��$��Center=�$
��Bold"�$��26�����	#FFEAECCC$��14(��� �� -     ��Name$  txtblCodeExtra��$��12(�$��Code and Description��$
��Bold"�$
��Wrap=�$��5,0,0,0q�$	��385��$��30���� $��5���� -   ! ��Name$! lvExtraInfo��$	��380��$��12(�
������ $��0q�$
��Auto=�$��Hidden=� " �� 
TargetType# �� Property$ �� Value��%4   '��       �� "" ��.��� # � y�$$ 	Collapsed=����� "" ��.��� # � ��$ �	#FFE4F2E1% �� View% �� .P��� & �� Width$& 40��' �� CellTemplate' 5  -System.Windows.Controls.ItemContainerTemplate�  .R��� $	��Top=����� *jobsl+�� $	& 535��'   .R�� $��Center=�$	��Top=�$	��525��.+��� $ Vertical=�.+�   
 wtxtBx#��[ $ Name of Company��$
��Left=�$��5,0,0,0q�$

 Wrap=�$
��Left=�$��22��$	��520������ *jobcom+�� $ 
Horizontal=�.+�  #��[ $ Designation��$
��Left=�$��5,0,0,0q�$

 Wrap=�$
��Left=�$��22��$	��205������ *jobdsg+�� $��From :��$��Right=�$��45��$��Normal$�&   Xceed.Wpf.Toolkit.DateTimePicker    StdDateTimePickerStyle01#��[ $��22��$	��120���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]](   Value( �� *sdate+�� $
��To :��$��Right=�$��30��$��Normal$�  #��[ $��22��$	��120��( �� *edate+  #��[ $ Major Duties��) J� 	AllowDrop) . * �� AcceptsReturn* . $
��Left=�$��5,0,0,0q�$

 Wrap=�$
��Left=�$��45��$	��520������ *	jobdesc+�� -   $ btnUpdateExtra��$��Update Info��$��25��$	��120��$��14(�$��Verdana)�  StdBtnStyle01#��[ �� -   $ chkAdEd��$��	Add/_Edit��$��13(�$��90,5,0,0q�$	��110���  . $��Slide=�$ 0��$ 30������ $ chkAdEd��$ 	IsChecked� �� $ chkAdEd��.���� $��0,0,5,5q�$��0q�$ 8������ $	 0,1&�$	 0,1&�.��� $ 	#FFECFBFB��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q�   $ Black��$ 5Q�$ 20Q�$	 0.8Q�$ Quality=�._��� -   $ CanCode��$	��390��$	��350������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4F2E1��$ 0.491Q�� $ 	#FFE4F2E1��$ 0.750Q�� $ 	#FF69B28B��$ 0.957Q�.+��� $	��390��$	��330��.+��� $ 
Horizontal=�$��0,10,0,0q�.+��� -   $  	txtblMnGr��$��12(�$��Group Description��$
��Bold"�$
��Wrap=�$��5,0,0,0q�$	��385��$��50���� �� -   $ spnlCodeEntry��{�.  $ Vertical=�$
��Left=�$��20,0,0,0q�.+��� $��Account Code :��$	��350���� $ 
Horizontal=�.+�  -   $	 txtActCode1��$��5,0,0,0q�$��20��+ �� 	MaxLength$+ 2��$

 Wrap=�$��25��,   AutoMoveFocus, . $ OnFocus=��� $��-��  -   $	 txtActCode2��$��5,0,0,0q�$��20��$+ 2��$

 Wrap=�$��25��, . $ OnFocus=��� $��-��  -   $	 txtActCode3��$��5,0,0,0q�$��20��$+ 4��$

 Wrap=�$��50��, . $ OnFocus=��� $��-��  -   $	 txtActCode4��$��5,0,0,0q�$��20��$+ 4��$

 Wrap=�$��50��, . $ OnFocus=��� -   - ��Name$- 
lblActCode��$��(XX-XX-XXXX-XXXX)�����	#FF987B7B�� $��5���� $��Description of Account :��$	��350���  �� $��20 �.+�  -   $	 	txtAcDesc��#��[ $ Description��$��5,0,0,0q�$

 Wrap=�$	��350��, . $ OnFocus=��� -   $ btnImgV��$��0��$��20��$��x��$��Right=�|�.  $y�	Collapsed=��� $��5���� $ 
Horizontal=�.+��� $��Type Code (if any) : ��$	��150���� -   $ 
chkELevel2��$"��Associate with resource code��$��13(�$	��200��$��20���  �� .+�  -   $	 
txtActtype��#��[ $ Type (if any)��$��5,0,0,0q�$��20��$

 Wrap=�$	��350��, . $ OnFocus=��� -   $ btnImgV2��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $��5���� $!��Type Description (if any) :��$	��350���  �� $	��18* ��� $
��157* � �� .+�  -   $	 txtActtdesc��#��[ $��0��$ Type Description (if any)��$��5,0,0,0q�$��20��$

 Wrap=�$	��350��, . $ OnFocus=�$��2���� -   $ btnImgV3��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=�$��1���� $��10���� $ 
Horizontal=�$��20,0,0,0q�.+��� $��5��$y�Hidden=��� -   $ btnAdd��$	��Add��$��25��$��60��$��14(�$��Verdana)�#��[ �� $��5��$y�Hidden=�|�.  �� -    $ btnEdit��$
��Edit��$��25��$��60��$��14(�$��Verdana)�#��[ �� $��5��$y�Hidden=�|�.  �� -!   $ 	btnUpdate��$
��Save��$��25��$��60��$��14(�{�.  $
  None��$��Verdana)�#��[ �� $��5��$y�Hidden=�|�.  �� -"   $ 	btnCancel��$��Cancel��$��25��$��60��$��14(�{�. $��Verdana)�#��[ �� $��15��$y�Hidden=�|�.  �� -#   $ chkPrint��$��_Print Code Book��$��13(�$��	190,5,0,0q�$	��120���  . $��Scroll=����� $ chkPrint��$ 	IsChecked� �� $ chkPrint��.���� $��2,2,5,5q�
����ح�   $ 	#FFC2C5C4��$ 5Q�$ 20Q�._��� $	��134��$	��158������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4F2E1��$ 0.491Q�� $ 	#FF69B28B��$ 0.957Q�.+��� -$   $ 	btnPrint1��$��Print��$��20��$��60��$��12(�$��Verdana)�#��[ . �� Left$. 38��
/ �� Top$	/ 116���� $��0,0,0,0q��� ��. $��0��$��1��$
��Auto=�$��Hidden=�.���� $ 
Horizontal=�$��0��$��1��$��2��.+��  �� $	��395 ��� $
��Auto �.+��� -%   $ stkp2��$
��Left=�$��0��$	��Top=�$��30,0,0,0q�.+��� -&   $ 	spnlLevel��$ 
Horizontal=�.+��� $��Level:��$��50��$
��Bold"�$��Center=��� -'   $ 	chkLevel1��$
��Main��$��60��0 d� 	IsChecked$
0 TrueY�{�.  $��Center=��� -(   $ 	chkLevel2��$��Sub-1��$��60��$
0 TrueY�$��Center=��� -)   $ 	chkLevel3��$��Sub-2��$��60��$
0 TrueY�$��Center=��� -*   $ 	chkLevel4��$��Details��$��60��$
0 TrueY�$��Center=�9� -+   1 9�Name$1 expAccAsset��  expAccStyle#��[   StretchyExpanderTemp#��[ $��_Assets��$  0��.��R� -,   2 R�Name$2 treeAccAsset��  treeAccStyle#��[ $  0��9� --   $1 
expAccLiab��#��[ #��[ $��_Liabilities��$  1��.��R� -.   $2 treeAccLiab��#��[ $  1��9� -/   $1 	expAccRev��#��[ #��[ $��	Re_venues��$  2��.��R� -0   $2 
treeAccRev��#��[ $  2��9� -1   $1 expAccDirCost��#��[ #��[ $��_Direct Costs��$  3��.��R� -2   $2 treeAccDirCost��#��[ $  3��9� -3   $1 
expAccOver��#��[ #��[ $��
_Overheads��$  4��.��R� -4   $2 treeAccOver��#��[ $  4��9� -5   $1 
expAccNoie��#��[ #��[ $��_Non-Oper.Income/Expense��$  5��.��R� -6   $2 treeAccNoie��#��[ $  5��9� -7   $1 	expAccFin��#��[ #��[ $��_Financial Expense��$  6��.��R� -8   $2 
treeAccFin��#��[ $  6��9� -9   $1 expAccOther��#��[ #��[ $��Ot_hers Head of A/c��$  7��.��R� -:   $2 treeAccOther��#��[ $  7���� -;   $ stklstv��$��1��$	��550��.+�  ASITHmsWpf.AcListView  -<   3  Name$
3 lvAc��  ASITHmsWpf.AcListView4   MouseDoubleClick$4 lvAc_MouseDoubleClick��5   KeyUp$5 
lvAc_KeyUp��$��14(�$	��550��$	��780��$
��Auto=�$��Hidden=����� "" ��.��� # � ��$ �	#FFE4F2E1�� # � ��$$ 20���� # � ��$$ 12(�% �� .P��� $& 45��$	��Sl#��'   .R��� $��30��$��0,-5,0,0q�$��Right=����� $ slnum��� $	& 110��$
��Code��6 �� DisplayMemberBinding6 �� $ actcode1��� $	& 320��6 �� $ actdesc����� $��5,0,0,0q�$
��Left=�$��Description���� $	& 100��6 �� $ acttype����� $
��Left=�$
��Type���� $	& 180��$��Type Description��6 �� $ acttdesc��� -=   $ 	stkp33333��$��Stretch=�$��2��$y�Hidden=�$	��Top=�
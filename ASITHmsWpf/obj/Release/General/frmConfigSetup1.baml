   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  'clr-namespace:ASITHmsWpf
ASITHmsWpf  7 clr-namespace:ASITHmsWpf.GeneralASITHmsWpf.General  (    "ASITHmsWpf.General.frmConfigSetup1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  $localclr-namespace:ASITHmsWpf  /localGen clr-namespace:ASITHmsWpf.General  -   
  � Tag$   Configuration Setup Screen��   IOUserControl#��[   � 	Resources   GridCellStyle1 �� 
TargetType*  $System.Windows.Controls.DataGridCell �� Property �� Value4   .ASITHmsWpf.General.AuthObjGridToolTipConverter  AuthDescToolTip��%:   &	       &	 !     �� "  .���  � |� .    .��� .+��� -    ��Name$ 	tabPanel1�� �� SelectedItem$ 4��$��Stretch=�$��Stretch=� �� TabStripPlacement$	 Top=�$��20, 20, 20, 60q�$��2��$��2��.@��� $��_User Configuration��$	��150��$��25��  StdTabItemStyle01#��[ .��� ���	#FFE4F2E1.+���  �� Orientation$ Vertical=�$��
0,10, 0, 0q�.+��� $ 
Horizontal=�.+�1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -   	  Name$	 AtxtEmployeeNam��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox
   MouseDoubleClick$&
  AtxtEmployeeNam_MouseDoubleClick��   
SearchType$ C��$
��Left=�$��22��$	��535�� � ToolTip$= 7Right Click or Double Click to view existing staff list�� � ContextMenu �� -    ��Name$ 
conMenuHCM��$	��400��$	��400���� -    ��Name$ btnFind��$��_Find & Show��$	��100��  StdBtnStyle01#��[ $��10,0,0,0q�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtPrivCopyFromID�� 
 wtxtBx#��[    	Watermark$ Source Staff ID��   SelectAllOnGotFocus . $��5,0,0,0q�$�� ��$
��Left=�$	��100�� �� 	MaxLength$ 12��$��0q�$��70,0,0,0q�$9 3Put Staff ID and click next for copy all privileges���� -   $ btnCopyPrivilege��$��50��$
��Copy��$��0q�|�.  #��[ �� -   $ btnUpdateUser��$	��150��$��Update Authentication��$6 0Update User Password & Remarks for existing user��$��50,0,0,0q�#��[ �� -	    ��Name$ stkUser��$ 
Horizontal=�$	��500��$��0,10,0,0q�.+��� $ Vertical=�$	��450��$	��Top=�.+��� $��Application users��$
��Bold"��� -
    ��Name$ ListBoxUserList�� � IsSharedSizeScope . $	��150��$	��350��$	��160������ .R��  � ColumnDefinitions �� $��20 � z� SharedSizeGroup$ Column1���� $
��Auto �$ Column2��.+��� $��0��$��Right=����� *slnum+  �� *hccode+ �� *
userrmrk+�� $��1�� �� Padding$ 5,0,0,0q����� *	signinnam+  �� *hccode+ �� *	namedsg+�� $ Vertical=�$	��370��$	��450��$	��Top=�$��10,0,0,0q�.+��� $��User description��$
��Bold"��� $ 
Horizontal=�.+��� -    ��Name$ 	imgPhoto2��$
��Left=�$��..\Images\NoUser.jpg��$y�	Collapsed=�$
��Fill=�$	��125��$	��140���� -   $ imgPhoto��$
��Left=�$��..\Images\NoUser.jpg��$y�Visible=�$
��Fill=�$	��125��$	��140���� $��5,0,0,0q�.+��� $ 
Horizontal=�.+��� $��Staff ID��$��70��$
��Left=��� $��:��$
��Left=��� -    ��Name$ lblHrId��$�� ��$	��150��$
��Left=�
��������� $ 
Horizontal=�$��0,2,0,3q�.+��� $��
Sign In ID��$��70��$
��Left=��� $��:��$
��Left=�  -   $ txtSignInId��#��[ $ Sign In Name�� . $��5,0,0,0q�$�� ��$
��Left=�$	��150��$ 20���� $ 
Horizontal=�.+��� $��Password��$��70��$
��Left=��� $��:��$
��Left=�G� -    G�Name$ txtUserPass1�� G� Password$  ��$	��150�� G� 	MaxLength$ 20��G� -   $ txtUserPass1h��$  ��$��0��$ 20��$y�	Collapsed=��� $ Vertical=�.+��� $��	Remarks :��$��70��$
��Left=�
���   �$��5,0,0,0q�  -   $ txtSignInRmrk��#��[ $ Remarks (if any)�� . $��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��40���� $ Vertical=�$��0,5,0,0q�$
��Left=�.+��� $ 
Horizontal=�.+��� $��Name & Designation : ��
���   �$
��Left=��� -   $ lblHrNamDsg��$
��Left=�$��50��$	��360��$
��Bold"��� $ 
Horizontal=�$��Right=�.+��� -   $ 
BtnUpPrmsn��$��22��$��80��$��Right=�$��Update��$y�	Collapsed=��� $ Vertical=�$	��Top=�$
��Left=�$	��450��$��10,0,0,0q�.+��� $ 
Horizontal=�.+��� $!��Privileges of selected user��$
��Bold"�&   System.Windows.Controls.DataGrid  -      Name$  
GrdPermisn��$	��430��$	��Top=�$��11(�
������ !   AutoGenerateColumns! .  $��0q�"   CanUserAddRows" .  #   CanUserResizeRows# .  $   SelectionMode$$ Single=�%   SelectionUnit$% FullRow=�$	��600��&   CanUserResizeColumns& .  '   CanUserDeleteRows' .  (   CanUserSortColumns( . )   ColumnHeaderStyle) �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��$
 Bold"���  � ��$ Stretch=���  � ��$
 Left=�*   Columns* 4  .System.Windows.Controls.DataGridTemplateColumn  ,	  &System.Windows.Controls.DataGridColumn+ 	  Header$+ Sl. #��, 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$, 40
 - 	  	CellStyle#- [ .   CellTemplate. �� .R��� $��Right=�$��0q�$��35������ *slnum+  $+ Module��$	, 120
 #- [ . �� .R��� $��0q�$	��115������ *
moduleid+  �� *
moduleid+  $, 50
 + �� -   $ lblHeaderAllow��$��Allow��$
  None��$/ )Double click here to Select/Unselect all ��. �� .R��� / ��Name$/ chkAllow��$��Center=�$��Center=�$��20��0 d� 	IsChecked0 �� 1 �� Path$1 objallow�2 �� Mode$2 TwoWay=�3 �� UpdateSourceTrigger$3 PropertyChanged=�  $+ Object Description��$	, 360
 #- [ . �� .R��� $��0q�$	��355������ *uidesc+ �� 4 �� 	Converter#4 [   �� *uicode+�� $��3��{�.  �� $��_Terminal Setup��$	��150��$��25��#��[ $	��Top=�.��� ���	#FFE4F2E1 �� $
��437* ��� $
��717* �.+��� $��Terminal Setup��$
��Left=�$��84.471,120,0,0q�$	��Top=�$��1���� $��3��{�.  �� $��Com_pany Information��$	��150��$��25��#��[ 5 J� RenderTransformOrigin$5 	0.525,1.2&�.��� ���	#FFE4F2E1 �� $
��393* ��� $
��761* �.+��� $��	20,10,0,0q�$��2��.+��� $ 
Horizontal=�.+��� $��Company Information��$
��Left=�$	��Top=��� $	��200��$y�Hidden=��� -   $/ chkLogo��$
��Logo��$��Center=�$��80��$
0 TrueY��� $��20��$y�Hidden=��� -   $/ chkLabel��$��Title Label��$��Center=�$	��100��$0 FalseY��� $��20��$y�Hidden=��� -   $ btnUpdateCompInfo��$��25��$��_Update��$	��100��#��[ �� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Company Logo :��$
��Left=�$��16(�$��Normal$��� -   $ imgLogo��$��..\Images\blue-bckgrd.jpg��$
��Left=�$y�Visible=�$
��Fill=�$	��181��$	��166���� $ 
Horizontal=�$��30��.+��� -   6 ��Name$6 txtLogo��{�.  $��22��$	��240���� -   $ btnUpdateLogo��$��20��$��Choose��$��60���� $��	50,50,0,0q�.+��� $��Company Info :��
����  �$
��Bold"��� $ 
Horizontal=�.+��� $��Company Name :��$	��150��$
��Bold"��� -   $ cmpname��$�� ���� $ 
Horizontal=�.+��� $��	Address :��$	��150��$
��Bold"��� -   $ 
cmpAddrsee��$�� ���� $ 
Horizontal=�.+��� $��
Phone No :��$	��150��$
��Bold"��� -   $ 
cmpPhoneno��$�� ���� $ 
Horizontal=�.+��� $��Email :��$	��150��$
��Bold"��� -   $ cmpEmail��$�� ���� $ Vertical=�.+��� $��Application Title Label :��$
��Left=�$��16(�$��Normal$��� -    $ imgLabel��$��..\Images\blue-bckgrd.jpg��$
��Left=�$y�Visible=�$
��Fill=�$	��586��$	��116���� $ 
Horizontal=�$��30��.+��� -!   $6 txtLabel��{�.  $��22��$	��240���� -"   $ btnUpdateLabel��$��20��$��Choose��$��60��$	��0,5q��� $��Branch Information��$	��150��$��25��#��[ $5 	0.525,1.2&�.��� ���	#FFE4F2E1.+��� $��	20,10,0,0q�.+��� $ 
Horizontal=�.+��� $��Branch Information :��$
��Left=�$
��Bold"�$	��Top=��� $	��200��$y�Hidden=��� -#   $/ 	chkLogo00��$
��Logo��$��Center=�$��80��$
0 TrueY��� -$   $/ 
chkLabel00��$��Title Label��$��Center=�$	��100��$0 FalseY��� $��20��$y�Hidden=��� $��20��$y�Hidden=��� -%   $ btnUpdateBranchInfo��$��25��$��_Update��$	��100��#��[ �� $��50��$y�Hidden=��� -&   $ 	Printinfo��$��25��$��_Print��$	��100��#��[ �� $ 
Horizontal=�.+��� $ Vertical=�$	��550��.+��� $ 
Horizontal=�.+��� $��Branch List   :��
����  �$
��Bold"��� -'   7 ��Name$7 cmbbrnch��8 �� SelectedIndex$8 0��$	��219���� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Branch Logo :��
����  �$
��Left=�$��16(�$��Normal$��� -(   $ 
imgBrnLogo��$��..\Images\blue-bckgrd.jpg��$
��Left=�$y�Visible=�$
��Fill=�$	��190��$	��169���� $ 
Horizontal=�$��30��.+��� -)   $6 
txtbrnLogo��{�.  $��22��$	��240���� -*   $ btnUpBrnLogo��$��20��$��Choose��$��60���� $��50,0,0,0q��� $ Vertical=�.+��� $��Branch Label :��
����  �$
��Left=�$��16(�$��Normal$��� -+   $ imgbrnLabel��$��..\Images\blue-bckgrd.jpg��$
��Left=�$y�Visible=�$
��Fill=�$	��450��$	��118���� $ 
Horizontal=�$��30��.+��� -,   $6 txtbrnLabel��{�.  $��22��$	��240���� --   $ btnBrnLabel��$��20��$��Choose��$��60��$	��0,5q��� $	��550��
����� �.+��� .+��� $��Section Information :��$
��Bold"�
����  ��� $��3��{�.  �� $��_Database Management��$	��150��$��25��#��[ .��� ���	#FFE4F2E1 �� $	��45* ��� $
��298* ��� $
��234* �.+��� $��3��.+��� $��Database Management��$
��Left=�$��10,10,10,10q�$	��Top=��� $ 
Horizontal=�$��30��.+��� $��Procedure Name:��$��24��$	��100���� -.   $7 	cmbSPName��$	��300��$��22���� -/   $ 	btnShowSP��$��22��$��Show SQL Script��$	��140���� $��20��$y�Hidden=��� -0   $ btnUpDateSP��$��22��$��Execute SQL Script��$	��140���� $��20��$y�Hidden=��� -1   $6 txtSPFileName��{�.  $��22��$	��280��$y�Hidden=��� -2   $ btnUploadSP��$��22��$��Choose SQL Script File��$	��140��$y�Hidden=��� $	��100��$y�Hidden=��� .+��� -3   $6 txtSqlScript��$��14(�9 �� AcceptsReturn9 . $	��370���� $��3��{�.  �� $��E_vent Log Configuration��$	��150��$��25��#��[ .��� ���	#FFE4F2E1.+��� $��Event Log Configuration��$
��Left=�$��521,120,0,0q�$	��Top=��� $��3��{�.  �� $��Data Backup��$	��150��$��25��#��[ .��� ���	#FFE4F2E1.+��� $��Data Backup��$
��Left=�$��521,120,0,0q�$	��Top=�
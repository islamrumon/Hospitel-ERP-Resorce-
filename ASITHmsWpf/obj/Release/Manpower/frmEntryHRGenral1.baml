   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  'clr-namespace:ASITHmsWpf
ASITHmsWpf  +    %ASITHmsWpf.Manpower.frmEntryHRGenral1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  $localclr-namespace:ASITHmsWpf  -   
  � Tag$=  7Human Resources General Information - Entry/Edit Screen��   IOUserControl#��[  .��� .+�� $
��Left=�$
��1190��$	��100��$	��Top=�.+���  �� Orientation$ Vertical=�$	��500��$
��Left=�.+��� $ 
Horizontal=�$��0,0,0,0q�.+��� -    ��Name$ btnNewRecord��$��_Add New��$��60��$
��Left=�  StdBtnStyle01#��[ �� $��5��$y�Hidden=�1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtEmployeeNam��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox   MouseDoubleClick$&  AtxtEmployeeNam_MouseDoubleClick��   
SearchType$ C��$
��Left=�$��22��$	��370�� � ToolTip$= 7Right Click or Double Click to view existing staff list�� � ContextMenu �� -    ��Name$ 
conMenuHCM��$	��400��$	��400���� $��5��$y�Hidden=��� -   $ btnFind��$��_Find��$��60��#��[ �� $ Vertical=�$��5,0,0,0q��� -   	 ��Name$	 StackUserinfo��$ 
Horizontal=�$��0,0,10,0q�$��Right=�$	��658��.+��� -   
 ��Name$
 	UserPhoto��$��../Images/NoUser.jpg��$y�Visible=�$
��Fill=�$	��100��$��90���� $��Center=�$ Vertical=�$��0,0,0,0q�$	��355��.+��� $ 
Horizontal=�.+��� ���	#FF000CFF$��Verdana)�$��11(�$��90��$
��Left=�$
��Name��$
��Bold"��� ���	#FF000CFF$��Verdana)�$��11(�$��20��$
��Left=�$��:��$
��Bold"��� -    ��Name$ lblShowfullname�����	#FF000CFF$��Verdana)�$��11(�$	��245��$
��Left=�$�� ��$��22���� $ 
Horizontal=�.+��� ���	#FF000CFF$��Verdana)�$��11(�$��90��$
��Left=�$��Designation��$
��Bold"��� ���	#FF000CFF$��Verdana)�$��11(�$��20��$
��Left=�$��:��$
��Bold"��� -	   $ lblShowdesig�����	#FF000CFF$��Verdana)�$��11(�$	��245��$
��Left=�$�� ��$��22���� $ 
Horizontal=�.+��� ���	#FF000CFF$��Verdana)�$��11(�$��90��$
��Left=�$��
Supervisor��$
��Bold"��� ���	#FF000CFF$��Verdana)�$��11(�$��20��$
��Left=�$��:��$
��Bold"��� -
   $ lblShowSuper�����	#FF000CFF$��Verdana)�$	��240��$
��Left=�$�� ��$��22���� $ Vertical=�$��Center=�.+��� $��80��#��["��� $��Center=�$ Vertical=�$��0,0,0,0q�$	��285��.+��� $ 
Horizontal=�.+��� ���	#FF000CFF$��Verdana)�$��11(�$��60��$
��Left=�$��HR ID��$
��Bold"��� ���	#FF000CFF$��Verdana)�$��11(�$��20��$
��Left=�$��:��$
��Bold"��� -   $ lblShowEMPID�����	#FFF50F0F$��Verdana)�$	��150��$
��Left=�$�� ��$��22��$
��Bold"��� $ 
Horizontal=�.+��� ���	#FF000CFF$��Verdana)�$��11(�$��60��$
��Left=�$��Card No��$
��Bold"��� ���	#FF000CFF$��Verdana)�$��11(�$��20��$
��Left=�$��:��$
��Bold"��� -   $ lblShowidCardnum�����	#FF000CFF$��Verdana)�$��11(�$	��150��$
��Left=�$�� ��$��22���� $ 
Horizontal=�.+��� ���	#FF000CFF$��Verdana)�$��11(�$��80��$
��Left=�$��Join Date :��$
��Bold"��� -   $ lblTJoinDate�����	#FF000CFF$��Verdana)�$��11(�$	��150��$
��Left=�$�� ��$��22��]� -    ]�Name$ DocPanelinfo��$��	0,100,0,0q�.+��� -    ��Name$ 	tabPanel1�� �� SelectedItem$ 4��$	��Top=�$��Stretch=�$��Stretch=� �� TabStripPlacement$	 Top=�.@��� $��_Basic Info��$��80��$��25��  StdTabItemStyle01#��[ .��� ���	#FFE4F2E1 � ColumnDefinitions �� $	��199 ��� $��9 ��� $	��397 ��� .+��� $ Vertical=�$
��Left=�$	��605��$��3��.+��� $ 
Horizontal=�
������.+��� $��Basic _HR Information ��$	��190��$��18(�$
��Bold"� �� Target ��  �� ElementName$ 
cmbhrgroup���� -    ��Name$ 
cmbhrgroup�� �� SelectedIndex$ 0��$��Bottom=�$��25��$	��410��$
��Bold"��� $ 
Horizontal=�.+��� $��HR ID��$	��100��$
��Left=�$��Normal$� �� $ 
cmbhrgroup���� $��:��$��Right=�$��20��$��Normal$��� -   $ lblHcmID��$��000000��$  000000000000��$	��130��$��Normal$�$
��Bold"����	#FFF00C0C|�.  �� $��ID Ca_rd No :��$��80��$��Right=�$��Normal$� �� $ txtIdCardNo��(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtIdCardNo�� 
 wtxtBx#��[    	Watermark$ Type ID Card no��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��22��$	��140���� $��	_Gender :��$��60��$��Right=�$��Normal$� �� $ 	cmbGender���� -   $ 	cmbGender��$��70��$ 0��$��23�� �� 
IsReadOnly . .@��� .��Male�� .��Female�� .��Other�� $ 
Horizontal=�$	��Top=�$��30��
������.+��� $��Emplo_yee Name��$	��100��$
��Left=�$
��Bold"�$��Normal$� �� $ cmbTname���� $��:��$��Right=�$��20��$��Normal$��� -   $ cmbTname��$ 0��$
��Left=�$��22��$��55��.@��� .��MR.�� .��MS.�� .��DR.�� $��5��$y�Hidden=�  -   $ txtFname��$��0��#��[ $ 
First Name��$��5,0,0,0q�$
 Wrap=�$�� ��$
��Left=�$��22��$	��100��$	��0,4q��� $��5��$y�Hidden=�  -   $ txtMname��$��0��#��[ $ Middle Name��$��5,0,0,0q�$
 Wrap=�$�� ��$
��Left=�$��22��$	��105���� $��5��$y�Hidden=�  -   $ 	txtLnamer��$��0��#��[ $ 	Last Name��$��5,0,0,0q�$
 Wrap=�$�� ��$
��Left=�$��22��$	��100���� $��5��$y�Hidden=�  -   $ 
txtSurname��$��0��#��[ $ Sur Name��$��5,0,0,0q�$
 Wrap=�$�� ��$
��Left=�$��22��$	��100���� $ 
Horizontal=�$	��Top=�$��30��.+��� $��	Full Name��$	��100��$
��Left=�$
��Bold"�$��Normal$�|�.  $��30��$	��Top=� �� $ txtFullName���� $��:��$��Right=�$��20��$��Normal$��� -   $ lblFullName��$
��Bold"�$	��346���� $��Blo_od Grrp:��$��Right=�$��80��$
��Left=�$��Normal$� �� $ cmbBldGr���� -   $ cmbBldGr��$��55��$��22��$ 0�� . .@��� .��A+�� .��B+�� .��AB+�� .��A-�� .��B-�� .��AB-�� .��O+�� .��O-�� $ 
Horizontal=�$	��Top=�$��30��
������.+��� $��D_esignation��$	��100��$
��Left=�$��Normal$� �� $ txtDesig���� $��:��$��Right=�$��20��$��Normal$�  -   $ txtDesig��$��22��$ C��$
��Left=�$	��350��
���   ��� $��
Gr_ade   :��$��60��$��Right=�$��Normal$� �� $ 
cmbHRGrade���� -   $ 
cmbHRGrade��$��70��$��22�� . $	��0,3q�$ 0���� $ 
Horizontal=�$	��Top=�$��30��.+��� $��_Depertment��$	��100��$
��Left=�$��Normal$� �� $ 
cmbWrkDept���� $��:��$��Right=�$��20��$��Normal$��� -   $ 
cmbWrkDept��$	��369�� . $��22��$ 0���� $ 
Horizontal=�$	��Top=�$��30��
������.+��� $��Super_visor Name��$	��100��$
��Left=�$��Normal$� �� $ 
AtxtSupCod���� $��:��$��Right=�$��20��$��Normal$�  -   $ 
AtxtSupCod��$ C��$
��Left=�$��22��$	��480��
���   �$	��0,4q��� $ 
Horizontal=�$	��Top=�$��30��.+��� $��_Joining Date��$	��100��$
��Left=�$��Normal$� �� $ xctk_joinDate���� $��:��$��Right=�$��20��$��Normal$�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_joinDate��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130���� $��Separation Notice Da_te :��$	��200��$��Right=�$��Normal$�$��30��$	��Top=� �� $ xctk_SepinfDat��  -    $ xctk_SepinfDat��#��[ $
��Bold"�$��22��$	��130���� $ 
Horizontal=�$	��Top=�$��30��
������.+��� $��Con_firm. Date��$	��100��$
��Left=�$��Normal$�$��30��$��Bottom=� �� $ xctk_joinConfarm���� $��:��$��Right=�$��20��$��Normal$�  -!   $ xctk_joinConfarm��#��[ $
��Bold"�$��22��$	��130���� $"��Separation Execut_ion Date :��$	��200��$��Right=�$��Normal$� �� $ xctk_SepexeDate��  -"   $ xctk_SepexeDate��#��[ $
��Bold"�$��22��$	��130���� $ 
Horizontal=�.+��  � RowDefinitions �� $��53 ���  �� $	��200 ��� .+��� $ Vertical=�$��0��$��0��$��2��.+��� -#   $
 imgPhoto��$
��Left=�$��..\Images\NoUser.jpg��$y�Visible=�$
��Fill=�$	��125��$	��140���� $ 
Horizontal=�$��30��.+��� -$    ��Name$ txtPhoto��{�.  $��22��$	��125���� -%   $ uploadphoto��$��20��$��Choose��$��50���� $��10��$y�Hidden=��� $ 
Horizontal=�$��1��$��1��.+��� $ Vertical=�.+��� $��Full Signature :��$��89��$
��Left=�$��Normal$� �� $ uploadphoto���� -&   $
 	signPhoto��$��..\Images\blue-bckgrd.jpg��$
��Left=�$y�Visible=�$
��Fill=�$	��125��$��60���� $ 
Horizontal=�$��30��.+��� -'   $ txtSign��{�.  $��22��$	��125���� -(   $ 
uploadSign��$��20��$��Choose��$��50���� $��40��$y�Hidden=��� $ Vertical=�.+��� $��Initial Signature :��$	��107��$
��Left=�$��Normal$� �� $ uploadphoto���� -)   $
 subsignPhoto��$��..\Images\blue-bckgrd.jpg��$
��Left=�$y�Visible=�$
��Fill=�$	��125��$��60���� $ 
Horizontal=�$��30��.+��� -*   $ 
txtsubSign��{�.  $��22��$	��125���� -+   $ uploadsubSign��$��20��$��Choose��$��50���� $ 
Horizontal=�$��1��$��0��$��
0,15,100,0q�$��Right=�.+��� $ 
Horizontal=�$	��Top=�$��30��.+��� $��Date of Birt_h��$��86��$
��Left=�$��Normal$�$
��Bold"� �� $ 	xctk_Date���� $��:��$��Right=�$��20��$��Normal$�  -,   $ 	xctk_Date��#��[ $
��Bold"�$��22��$	��130���� --   $ lblage��$��Age: (XX years)��$
��Left=�$��Normal$�$��30��$	��Top=� �� $ txtFullName���� $��0,0,0,0q�$ 
Horizontal=�$��3��.+��� $	��500��#��["��� $ Vertical=�$
��Left=�$��3��.+��� .+��� $#��Others Personal Information :��$��5,0,0,0q�$
��Bold"�&   System.Windows.Controls.DataGrid  -.     Name$
 dgv1��$	��360��$	��570��$
��Left=�   AutoGenerateColumns .  $��	10,10,0,0q�$��0q�   CanUserResizeRows .     
IsReadOnly .     Columns  0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$	! Sl#��"   CanUserSort" .  # +� IsFocusScope# .  1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ �� *slnum+  $! Title��%   Width5	  /System.Windows.Controls.DataGridLengthConverter$	% 170	 &   
IsReadOnly& . " .  $ �� ' �� Path$' 	TitleDesc�4
  .System.Windows.Controls.DataGridTemplateColumn
  $! Description��$	% 350	 ( 
  CellTemplate( �� .R��� $��0q�$	��350������ *	ValueDesc+) �� Mode$) TwoWay=�* �� UpdateSourceTrigger$* PropertyChanged=�  �� *Code+�� $ 
Horizontal=�$��	0,15,50,0q�$��Right=�.+��� -/   $ btnUpdateInfo��$��25��$��_Update Basic Info��$	��120��#��[ + J� RenderTransformOrigin$+ -3.357,2.496&��� $��3��{�.  �� $��_Qualification��$��80��$��25��#��[ $	��Top=�.��� ���	#FFE4F2E1.+��� $ Vertical=�$
��Left=�$	��780��$��0,10,0,0q�.+��� $��Basic Education:��$	��110��$
��Left=�$��Normal$�$
��Bold"�  -0   $ 	dgViewEdu��
������ $	��120��$��12(�$
��Auto=�$��Hidden=� .  ,   CanUserSortColumns, .   .   
  $	% 120	 $! Name of Exam��( �� .R�� $
��Left=�$	��Top=�.+�  #��[ $
��Left=�$��5,0,0,0q�$��0q�$
 Wrap=�$
��Left=�$	��120�� �� $' 	degreenam����� $' examnam�$) TwoWay=�$* PropertyChanged=�
  $	% 200	 $! Name of Institute��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��200������ $' examinst�$) TwoWay=�$* PropertyChanged=�
  $	% 120	 $! Period��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��120������ $' 	eduperiod�$) TwoWay=�$* PropertyChanged=�
  $% 70	 $
! Year��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$��70������ $' examyear�$) TwoWay=�$* PropertyChanged=�
  $% 90	 $! Result��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$��90������ $' 
examresult�$) TwoWay=�$* PropertyChanged=�
  $	% 150	 $! Remarks��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��150������ $' examrmrk�$) TwoWay=�$* PropertyChanged=��� $��5��$y�Hidden=��� $��Higher Education:��$	��110��$
��Left=�$��Normal$�$
��Bold"�  -1   $ 
dgViewHrEd��
������ $	��100��$��12(�-   HeadersVisibility$	- Row=�$
��Auto=�$��Hidden=� .  , .   .   
  $	% 120	 $! Name of Exam��( �� .R�� $
��Left=�$	��Top=�.+�  #��[ $
��Left=�$��5,0,0,0q�$��0q�$
 Wrap=�$
��Left=�$	��120�� �� $' 	degreenam����� $' examnam�$) TwoWay=�$* PropertyChanged=�
  $	% 200	 $! Name of Institute��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��200������ $' examinst�$) TwoWay=�$* PropertyChanged=�
  $	% 120	 $! Period��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��120������ $' 	eduperiod�$) TwoWay=�$* PropertyChanged=�
  $% 70	 $
! Year��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$��70������ $' examyear�$) TwoWay=�$* PropertyChanged=�
  $% 90	 $! Result��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$��90������ $' 
examresult�$) TwoWay=�$* PropertyChanged=�
  $	% 150	 $! Remarks��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��150������ $' examrmrk�$) TwoWay=�$* PropertyChanged=��� $��5��$y�Hidden=��� $��Training/Others:��$	��110��$
��Left=�$��Normal$�$
��Bold"�  -2   $ dgViewExtEd��
������ $	��100��$��12(�$	- Row=�$
��Auto=�$��Hidden=�, .   .   .   
  $	% 120	 $! Name of Exam��( �� .R�� $
��Left=�$	��Top=�.+�  #��[ $
��Left=�$��5,0,0,0q�$��0q�$
 Wrap=�$
��Left=�$	��120�� �� $' 	degreenam����� $' examnam�$) TwoWay=�$* PropertyChanged=�
  $	% 200	 $! Name of Institute��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��200������ $' examinst�$) TwoWay=�$* PropertyChanged=�
  $	% 120	 $! Period��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��120������ $' 	eduperiod�$) TwoWay=�$* PropertyChanged=�
  $% 70	 $
! Year��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$��70������ $' examyear�$) TwoWay=�$* PropertyChanged=�
  $% 90	 $! Result��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$��90������ $' 
examresult�$) TwoWay=�$* PropertyChanged=�
  $	% 150	 $! Remarks��( �� .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��0q�$	��150������ $' examrmrk�$) TwoWay=�$* PropertyChanged=��� $ Vertical=�$
��Left=�$��3��$��	785,0,0,0q�.+��� $��5��$	��500��$y�Hidden=��� $ Vertical=�$��Right=�$	��400��$��0,10,0,0q�.+��� $%��Job Experiance: (Reverse Order)��$
��Left=�$��Normal$�$
��Bold"�  -3   $ dgvJob1��
������ $	��390��$	��400��$
��Left=� .  $��0,0,0,0q�$	- Row=�$��0q� .  $��Hidden=� . $
��Auto=�  
  $	! Sl.��$% 30	 ( �� .R��� $	��Top=�$
��Wrap=����� *jobsl+
  $! Description of Jobs��$	% 350	 ( �� .R�� .+��� $ Vertical=�.+�  #��[ $ Name of Company��$
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��22��$	��340������ *jobcom+$) TwoWay=�$* PropertyChanged=��� $ 
Horizontal=�.+�  #��[ $ Designation��$
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��22��$	��170������ *jobdsg+$) TwoWay=�$* PropertyChanged=�  #��[ $ 
Department��$
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��22��$	��170������ *	jobdept+$) TwoWay=�$* PropertyChanged=��� $ 
Horizontal=�.+��� $��From:��$��Right=�$��Normal$�$��50��  #��[ $��22��$	��120���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]].   Value. �� *sdate+$) TwoWay=�$* PropertyChanged=��� $	��To:��$��Right=�$��Normal$�$��30��  #��[ $��22��$	��120��. �� *edate+$) TwoWay=�$* PropertyChanged=�  #��[ $ Major Duties��/ J� 	AllowDrop/ . 0 �� AcceptsReturn0 . $
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��90��$	��340������ *	jobdesc+$) TwoWay=�$* PropertyChanged=�  #��[ $ Remarks (if any)��/ . 0 . $
��Left=�$��5,0,0,0q�$
 Wrap=�$
��Left=�$��30��$	��340������ *
jobrmrks+$) TwoWay=�$* PropertyChanged=��� $ 
Horizontal=�$��	0,15,50,0q�$��Right=�.+��� -4   $ btnUpdateQualif��$��25��$��_Update Qualification��$	��120��#��[ �� $��3��{�.  �� $��_Payroll & Office Time��$	��120��$��25��#��[ $+ 	0.525,1.2&�.��� ���	#FFE4F2E1.+��� $ Vertical=�$
��Left=�$	��550��$��10,0,0,0q�.+��� $��Payroll Information��$��Center=�$��Stretch=�$��0,5,0,0q�$
��Bold"�
��������� $��Mongthly Addition :��$
��Bold"�  -5   $ 	dgvPayAdd�� .  
������  . $��	100,0,0,0q�$	��Top=�$	��150��$	��400��$
��Auto=�$
��Left=�$��Hidden=�, .      $% 30	 $	! Sl#��# .  " .  $ �� $' slnum�  $	% 200	 " .  $ �� $' 	TitleDesc�1   HeaderTemplate1 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��|�.  $	��180��
  $	% 140	 $! Amount��( �� .R��� -6   $��0q�$��Right=�$	��120������ $' 	ValueDesc�$) TwoWay=�$* PropertyChanged=�  �� $
' Code��� $ 
Horizontal=�.+��� $��Monthly Gross Pay:��$	��180��$��Right=�$
��Bold"�$��	120,0,0,0q��� -7   $ 	lblGTotal��$��-��$��Right=�$	��175��$
��Bold"��� $��Mongthly Deduction :��$
��Bold"�  -8   $ 	dgvPayDed��
������ $	��400�� . $��	100,0,0,0q�$
��Left=�$	��Top=�$	��100��$��12(�$
��Auto=�$��Hidden=� .  , .  $	- Row=�    $% 30	 $	! Sl#��# .  $ �� $' slnum�  $	% 200	 $ �� $' 	TitleDesc�1   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��|�.  $	��180��
  $	% 140	 $! Amount��( �� .R��� $ txtAmt��-9   $��0q�$��Right=�$	��120������ $' 	ValueDesc�$) TwoWay=�$* PropertyChanged=�  �� $
' Code��� $ 
Horizontal=�.+��� $��Monthly Total Detuction:��$	��180��$��Right=�$
��Bold"�$��	120,0,0,0q��� -:   $ lblGDed��$��-��$��Right=�$	��175��$
��Bold"��� $ 
Horizontal=�.+��� $��Monthly Net Pay:��$	��180��$��Right=�$
��Bold"�$��	120,0,0,0q��� -;   $ 	lblNetPay��$��-��$��Right=�$	��175��$
��Bold"��� $ 
Horizontal=�$��0,0,0,0q�.+��� $��Ban_k: ��$
��Bold"�$��Right=� �� $ txtBankName��  -<   $ txtBankName��$	��280��$��22��#��[ $ 	Bank Name��$
 Wrap=�$�� ��$
��Left=��� $��A/c: ��$
��Bold"�$��Right=� �� $ txtBankAccNo��  -=   $ txtBankAccNo��$	��120��#��[ $��22��$ 
A/C Number��$
 Wrap=�$�� ��$
��Left=��� $ Vertical=�$
��Left=�$	��550��$��	600,0,0,0q�.+��� $(��"Overtime & Others Rate Information��$��Center=�$��Stretch=�$��0,5,0,0q�$
��Bold"�
��������� $ Vertical=�.+�  ->   $ dgvPayOther�� .  
������  . $��	100,0,0,0q�$	��Top=�$	��150��$	��400��$
��Auto=�$
��Left=�$��Hidden=�, .      $% 30	 $	! Sl#��# .  " .  $ �� $' slnum�  $	% 200	 " .  $ �� $' 	TitleDesc�1   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��|�.  $	��180��
  $	% 140	 $! Rate/Amount��( �� .R��� -?   $��0q�$��Right=�$	��120������ $' 	ValueDesc�$) TwoWay=�$* PropertyChanged=�  �� $
' Code��� $ Vertical=�$��0,20,0,0q�.+��� $��Office Timing Information��$��Center=�$��Stretch=�$��0,5,0,0q�$
��Bold"�
�������  -@   $ dgvAttn�� .  
������  . $��	100,0,0,0q�$	��Top=�$	��150��$	��400��$
��Auto=�$
��Left=�$��Hidden=�, .      $% 30	 $	! Sl#��# .  " .  $ �� $' slnum�  $	% 250	 " .  $ �� $' 	TitleDesc�1   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��|�.  $	��200��
  $	% 100	 $! Time/Day��( �� .R��� $��0q�$��Right=�$��80������ $' 	ValueDesc�$) TwoWay=�$* PropertyChanged=�  �� $
' Code��� $ 
Horizontal=�$
��Left=�$��160,60,20,0q�.+��� -A   $ btnCalculate��$	��100��$��25��$��_Recalculate��#��[ �� $��20��$y�Hidden=��� -B   $ btnPayrollUpdate��$	��Top=�$��25��$��_Update��$	��100��#��[ �� $��3��{�.  �� $��
_Summaries��$��80��$��25��#��[ .��� ���	#FFE5E5E5.+��� $ Vertical=�.+��� $0��*Summary Reports of Human Capital Managment��$��Stretch=�$��Center=�$	��Top=��� $ 
Horizontal=�.+��� -C   $ btnGenInfo1��$	��150��$��Show General Information��$��10,0,0,0q��� -D   $ btnLeaveForm1��$	��150��$��Show Leave Form��$��10,0,0,0q��� -E   $ 
btnParoll1��$	��150��$��Show Pay Slip��$��10,0,0,0q��� -F   $ btnAttnJob1��$	��150��$��Show Attendence Job Card��$��10,0,0,0q��� -G   $ btnMontSalSheet��$	��150��$��Monthly Salary Sheet��$��10,0,0,0q��� -H   $ btnAbstMontSal��$	��150��$ ��Abstruct Of Monthly Salary��$��10,0,0,0q��� -I   $ btnAbstMontDeducFrm��$	��170��$#��Abstruct Of Monthly Deduction��$��10,0,0,0q��� $ Vertical=�$��10q�.+��� -J   2 ��Name$2 ChkEdu��$��With EducationInfo��3 d� 	IsChecked$
3 TrueY��� -K   $2 ChkJob��$��With jobInfo��$
3 TrueY�
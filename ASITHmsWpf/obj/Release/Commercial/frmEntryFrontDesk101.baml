   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  0    *ASITHmsWpf.Commercial.frmEntryFrontDesk101   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4UC%clr-namespace:ASITHmsWpf.UserControls     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -   
  � Tag$  ORDER RECEIPT��$��14(�.��� .+�� -   $��GridDataEntry��.+���  �� Orientation$ Vertical=�$	��570��$	��Top=���L� .+��� $ 
Horizontal=�$��5,5,0,0q�.+��� -    ��Name$ stkpTitleEntry��$	��700��.+��� -   $ 
stkpTitle1��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkOldPatient��$��Pre_vious ID :��$��Center=�$	��100�����	#FF0720B9|�.  (  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtPrevTransID��$	��110��   	Watermark$ Existing Transaction ID�� 
  wtxtBx#��[  $��2,0,0,0q�$��22��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=� �� 	MaxLength$ 11��{�.  
��������� -   	 ��Name$	 
btnOldShow��$��50��$
��Show��$��24��$��5,0,10,0q�$y�Visible=�
�������$��0q�
������ �� -   $	 btnFindPatient��$	��100��$��
_Find More��$��0q�
������ 
�������|�.  �� -	   $ chkBranchName��$��Branc_h Name :��$��Center=�$	��110�����	#FF0720B9|�.  $��20,0,0,0q��� -
   
 ��Name$
 	cmbBranch��$	��190��$��24�� �� SelectedIndex$ 0��
�������$��0q�
������ {�.  .@��� $��
Branch-001��$
  1101���� $��
Branch-002��$
  1102���� $��
Branch-003��$
  1103���� 
����  �$	��700���� -   $ 
stkpTitle2��.+��� $ 
Horizontal=�.+��� $��Trsaction ID :��$	��100��$��Right=��� -    ��Name$ 
txtTransID��$	��110��$��22��
��������� -    ��Name$ lblPatientName��$��Pati_ent Name :��$	��100��$��Right=� �� Target ��  �� ElementName$ txtPatientName��  -   $ txtPatientName��$	��390��$ Name of patient��#��[  $��5,0,0,0q�$��22�� . $ OnFocus=� �� CharacterCasing$ Upper=�
��������� $ 
Horizontal=�.+��� $��Patient Age :��$	��100��$��Right=�%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$ iudAgeY���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Maximum$	 120X�   Minimum$ 0X�   Value$ 0X�$��50��$��22���  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ 00��
��������� $��Y��$��12(�$��Center=�$��20��  -   $ iudAgeM��$ 12X�$ 0X�$ 0X�$��40��$��22��$ 00��
��������� $��M��$��12(�$��Center=�$��20��  -   $ iudAgeD��$	 120X�$ 0X�$ 0X�$��40��$��22��$ 00��
��������� $��D��$��12(�$��Center=�$��20���� $��	_Gender :��$��80��$��Right=� �� $ cmbPatientGender���� -   $
 cmbPatientGender��$	��100��$��24��$ 0��
�������$��0q�
������ .@��� $
��Male���� $��Female���� $��Other���� $��
P_hone # :��$��80��$��Right=� �� $ txtContactNo��  -   $ txtContactNo��$	��150��$ Contact phone��#��[  $��5,0,0,0q�$��22�� . $ OnFocus=�$ 20��
��������� $ 
Horizontal=�.+��� -   $ chkMemberID��$��Mem_ber ID :��$��Center=�$	��100�� d� 	IsChecked$ FalseY�|�.  $��Center=�  -   $ txtMemberID��$	��170��$ Membership no��#��[  $��5,0,0,0q�$��22�� . $ OnFocus=�{�.  $ 20��
��������� -   $ chkDeliveryTime��$��_Delivery Date & Time :��$��Center=�$	��160��$ FalseY�|�.  $��Center=�$��40,0,0,0q�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xcdtDeliveryDT��$	��230��$��24��&   Xceed.Wpf.Toolkit.DateTimeUpDown   Format$ Custom=�{�.     FormatString$ ddd dd-MMM-yyyy hh:mm tt��   TimePickerVisibility$ Hidden=�   AutoCloseCalendar . 
��������� $ 
Horizontal=�.+��� -   $ lblRefByName��$��_Referred By :��$	��100��$��Right=� �� $ txtRefByName���� -   $ 
txtRefByID��$	��110��
�������$��22��  -   $ txtRefByName��$	��490��$4 .Referal Name (Doctor/Company/Person/Self etc.)��#��[  $��22�� . $ OnFocus=�$ Upper=�
��������� $	��120��$	��135��$	��Top=�$��5,0,0,0q�.+��� -    ��Name$ imgEmptyPhoto��$-��'/ASITHmsWpf;component/Images/NoUser.jpg��$y�Hidden=�$��0��$��0���� -   $	 btnPatPhoto��$��0q�|�.  .���� -   $ imgPatPhoto��$
��Fill=�$	��135��$	��120��$  000000���� $	��235��$��0,0,0,0q�.+��� $ 
Horizontal=�
�������$
��Left=�.+��� $��Date :��$��50��$
��Left=�$��Right=�$
��Bold"��� -   $ lblTransDate��$��09-Nov-2016, Wednesday��$
��Left=�$
��Left=�$
��Bold"��� $ 
Horizontal=�$��8,5,0,0q�.+��� $
��Left=�.+��� -   $	 
btnNewShow��  StdBtnStyle01#��[ $��90��$
��_New��$	  New���� -    $	 btnUpdateTrans��#��[ $��90��$��0,5,0,5q�$��_Save��$	  New���� -!   $	 btnPrintTrans��#��[ $��90��$��_Print�� � ToolTip$ (F12)���� -"   $ chkPrintDirect��$��
To Printer��$��11(�$��0,5,0,0q��� $��20,0,0,0q�.+��� -#    ��Name$ 	lstTrans1��$	��110��$	��100��$��0q�
������ $��11(�$��Center=�$
��Auto=�$��Hidden=�.@��� $��90��$��HO161100031��$��0q��� $��90��$��HO161100027��$��0q��� $��90��$��HO161100021��$��0q��� $��90��$��HO161100017��$��0q��� $��90��$��HO161100011��$��0q��� $��90��$��HO161100005��$��0q��� $��90��$��HO161100001��$��0q��� -$   $ 	xstkEntry��$��5,5,0,0q�$y�Visible=�.+��� .+�&   System.Windows.Controls.DataGrid  -%     Name$ dgvMemo��    CanUserResizeRows  .  
�������!   
IsReadOnly! .  $��0q�$��0,0,0,0q�$��12(�$	��Top=�$	��195��$
��Auto=�"   AutoGenerateColumns" .  #   CanUserAddRows# .  $   CanUserDeleteRows$ .  $��Hidden=�%   SelectionUnit$
% Cell=�&   SelectionMode$& Single=�'   FrozenColumnCount$' 2��(   Columns( 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumn) 
  Width5  /System.Windows.Controls.DataGridLengthConverter$) 30 * 
  Header$	* Sl#��+ +� IsFocusScope+ .  , 
  CanUserSort, .  - 
  
IsReadOnly- . . 
  CanUserReorder. .  / 
  CanUserResize/ .  1  +System.Windows.Controls.DataGridBoundColumn0   Binding0 �� 1 �� Path$1 slnum�4  .System.Windows.Controls.DataGridTemplateColumn  $	) 220 2 
  HeaderTemplate2 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��
Group Name��|�.  $	��180��3   CellTemplate3 �� .R��� $	��220��$��22������ $1 gsirdesc� �� $1 gsirdesc�  $	) 340 2   .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��	Item Name��|�.  $	��180��3 �� .R��� $	��340��$��22������ $1 sirdesc� �� $1 sirdesc�  $) 40 $
* Unit��3 �� .R��� $��50��$��22������ $1 sirunit�  $) 40 - .  * �� $	��Qty��$��30��$��10,0,0,0q�3 �� .R�  -&   4 �� TextAlignment$4 Right=� . $��0q�$��25��$ OnFocus=�$��5,0,0,0q�#��[  $ Quantity������ $1 itemqty�5 �� Mode$5 TwoWay=�6 �� UpdateSourceTrigger$6 PropertyChanged=�7 �� StringFormat$7 
{0: #,##0}��  $) 80 * �� $
��Rate��$��50��$��20,0,0,0q�3 �� .R�� $��Right=�$	��Top=�$��70��.+��� $��Right=�$	��Top=����� *	salrate+$7 {0: #,##0.00}��  $	) 100 * �� $��Amount��$��60��$��30,0,0,0q�3 �� .R�� $��Right=�$	��Top=�$��90��.+��� $��Right=�$	��Top=����� *salam+$7 {0: #,##0.00}��  $) 75 * �� $��Discount��$��60��$��10,0,0,0q�3 �� .R�  -'   $4 Right=� . $��0q�$��65��$ OnFocus=�$��5,0,0,0q�#��[  $ Discount������ $1 idisam�$5 TwoWay=�$6 PropertyChanged=�$7 {0: #,##0.00}�� �� *	icdisam+$7 {0: #,##0.00}��  $	) 100 * �� $��
Net Amount��$��80��$��10,0,0,0q�3 �� .R�� $��Right=�$	��Top=�$��90��.+��� $��Right=�$	��Top=����� *inetam+$7 {0: #,##0.00}���� $ 
Horizontal=�.+��� $ 
Horizontal=�$	��Top=�.+��� $��_Item :��$��Right=�$��60�� �� $ txtItemName��  -(   $ txtItemName��$	��300��$��24��$��12(�$ Name of Item�� . $ OnFocus=�
��������� -)   $	 
btnAddItem��$	��Add��$��40��$��24��$��0q�
��������� $��10,0,0,0q�
�������.+��� $ 
Horizontal=�.+��� -*   $ chkDiscount��$��	Disco_unt��$��80��|�.  $��5,0,0,0q�$��Center=�$  Xceed.Wpf.Toolkit.DoubleUpDown  -+   8  Name$8 	iudDisPer���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]9   Maximum$	9 100X�:   Minimum$: 0X�;   Value$; 0X�$��60��$��22��|�. �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]<   	Increment$< 1X�=   FormatString$= 00.00��{�.  
��������� $��%��$��25��$
��Left=��� -,   $	 btnSetDispPer��$��Set All��$��50��$��22��$��0q�
������ {�.  �� $ 
Horizontal=�.+��� --   $ chkDisDoctor��$��Doctor's part only��$	��140��|�.  $
��Left=�$��85,5,0,0q��� $��10,0,0,0q�.+��� $ 
Horizontal=�$	��Top=�.+��� -.   $	 btnTotal��$	��110��$��_Total :��$��Right=�$��24��$
��Bold"�
����  �
�������$��0q�
������ �� -/   $ lblGrandTotal��$��0,00,000.00��$��Right=�$	��100��
�������$
��Bold"�
����  ��� -0   $ lblDiscTotal��$��	00,000.00��$��Right=�$��75��$
��Bold"�
����  ��� -1   $ lblNetTotal��$��0,00,000.00��$��Right=�$	��100��
�������$
��Bold"�
����  ��� $ 
Horizontal=�.+��� $��CC Charge :��$��Right=�$	��110��$
��Bold"�  -2   $ txtCCCharge��$	��100��#��[  $��5,0,0,0q�$��22�� . $ OnFocus=�
��������� $��	CC Paid :��$��Right=�$��75��$
��Bold"�  -3   $ 	txtCCPaid��$	��100��#��[  $��5,0,0,0q�$��22�� . $ OnFocus=�
��������� $ 
Horizontal=�.+��� $��Bottom=�.+��� $��Remar_ks��$��Right=�$
��Left=� �� $ 
txtRemarks��  -4   $ 
txtRemarks��$	��400��$��60��> �� AcceptsReturn> . $	 140��? �� TextWrapping$
? Wrap=�
��������� $��10,0,0,0q�.+��� $ 
Horizontal=�.+��� $��
Pa_yments:��$
��Left=�$
��Left=�$��80��$��0q� �� $ 
dgvColInfo��  -5   $ 
dgvColInfo��  .  
�������! .  $��0q�$	��570��@   RowHeaderWidth$@ 0Q�$	��Top=�$	��120��$
��Auto=�" .  # .  $ .  $��Hidden=�$
% Cell=�( 	  $	) 140 $* Date & Time��+ .  , .  - . . .  / .  A 	  FontSize$A 12Q�0 �� $1 coldat�$7 {0:dd-MMM-yyyy hh:mm tt}��  $	) 300 - .  * �� $��Description��$	��140��$��10,0,0,0q�3 �� .R�  $
4 Left=� . $��0q�$	��290��$ OnFocus=�$��0,0,0,0q�#��[  $ Payment Description��B �� 
IsReadOnlyB �� $1 colstat����� $1 coldesc�$5 TwoWay=�$6 PropertyChanged=�  $	) 100 * �� $��Paid Amount��$��80��$��10,0,0,0q�3 �� .R�  -6   $��Right=�$	��Top=�#��[  $��90��$��0q�$ OnFocus=�$��0,0,0,0q�B �� $1 colstat����� $1 colam�$7 {0: #,##0.00}��$5 TwoWay=�$6 PropertyChanged=��� $ 
Horizontal=�$	��Top=�.+��� $��	Balance :��$��Right=�$��80��$
��Bold"�
���  ���� -7   $ lblNetBalance��$��0,00,000.00��$
��Left=�$	��120��
�������$
��Bold"�
���  ���� $��Total Bill :��$��Right=�$��80��$
��Bold"�
����  ��� -8   $ lblTotalBill��$��0,00,000.00��$��Right=�$	��120��$
��Bold"�
����  �
��������� $��Paid Amount :��$��Right=�$	��120��$
��Bold"�
����  ��� -9   $ lblTotalPaid��$��0,00,000.00��$��Right=�$	��100��$
��Bold"�
����  �
�������� -:   $��GridFindPatient��$y�	Collapsed=����� C �� Opacity$	C 0.4Q�D �� Color$D 	LightGray��.+��� $��Center=�$��Center=�$	��800��$	��400��
���   �$��1q�
�������E �� Padding$E 1q�._��� ���� F �� EndPoint$F 0.5,1&�G �� 
StartPoint$G 0.5,0&�.��� H � Color$H 	#FFE4F2E1��I � Offset$I 0.491Q�� $H 	#FF69B28B��$I 0.957Q�.+��� $
��Left=�$
��Wrap=�$��Enter Number Of Items���� -;   $��	countText��$
��Left=�$��10��$
? Wrap=�$	��0,3q�$	��100���� -<   $	 btnCloseFindPatient��$
��Left=�$��75��$��Bac_k to Entry��J �� IsCancelJ . � -=   K �Name$K GridRefrrerList��$��105,145,240,220q�$y�	Collapsed=�$	��Top=�.+��� $ 
Horizontal=�$	��200��$	��Top=�
�������.+�� L � ColumnDefinitionsL �� $��40 ��� $
��Auto �.+��� $��0��.+��� ->   $	 btnAddRefBy��$��40��$	��Add��$��0q�
������ 
������ $��Right=�|�.  $��0,5,0,0q��� -?   $	 btnCloseRefBy��$��40��$��Close��$��0q�
������ 
������ $��Right=�|�.  $��0,10,0,0q��� -@   $ stkpRefByList��$��1��$ 
Horizontal=�$y�Visible=�$	��685��.+��� $��70��.+��� $��Ref. _List :��$��70��$��Right=� �� $ lstReferrer���� -A   $	 btnRefreshRef��$��60��$��Refresh��$��0q�
������ 
������ $��0,10,0,0q�$��Right=�|�.  �� -B   $ lstReferrer��$	��200��$	��610��
������ $	��Top=�$
��Auto=�$��Hidden=�.@��� $��Referrer Name - 001��$	  001���� $��Referrer Name - 002��$	  002���� $��Referrer Name - 003��$	  003���� $��Referrer Name - 004��$	  004���� $��Referrer Name - 005��$	  005���� $��Referrer Name - 006��$	  006���� $��Referrer Name - 007��$	  007���� $��Referrer Name - 008��$	  008���� $��Referrer Name - 009��$	  009���� $��Referrer Name - 010��$	  010���� $��Referrer Name - 011��$	  011���� $��Referrer Name - 012��$	  012���� -C   $ stkpRefByEnry��$��1��$y�Hidden=�$	��225��$	��685��$	��Top=�
������ .+��� $ 
Horizontal=�.+��� $��Refd. By ID :��$	��100��$��Right=��� -D   $ lblRefByID1��$��60��
�������$��12(�$��1q����	#FFABADB3$��24���� $��Name Title :��$	��100��$��Right=��� -E   $
 cmbRefByTitle��$	��170��$��0q�
������ $ 0��$��22��.@��� $��(None)��$  .���� $��DOCTOR (DR.)��$	  DR.���� $��PROFESSOR (PROF.)��$  PROF.���� $"��PROFESSOR DOCTOR (PROF. DR.)��$  	PROF. DR.���� $��HOSPITAL / CLINIC��$  HOS/CL���� $��MEDICINE SHOP��$  FARMA���� $��
INDIVIDUAL��$  INDV.���� $��OTHER COMPANY��$  COMPANY���� $��Refd. Type :��$��90��$��Right=��� -F   $
 cmbRefByType��$	��110��$��0q�
������ $ 0��$��22��.@��� $��GENERAL��$  GENERAL���� $��	CORPORATE��$  	CORPORATE���� $��SPECIAL��$  SPECIAL���� $��OTHERS��$  OTHERS���� $ 
Horizontal=�.+��� $��Name :��$	��100��$
��Left=�$��Right=��� -G   $ lblRefByNameTitle1��$��.��$	��100��
�������$��12(�$��1q����	#FFABADB3$��24��  -H   $ txtRefByName1��$	��430��$4 .Referal Name (Doctor/Company/Person/Self etc.)��#��[  $��24�� . $ OnFocus=�$ Upper=��� $ 
Horizontal=�.+��� $��Title/Degree :��$	��100��$
��Left=�$��Right=�  -I   $ txtRefByQCTitle1��$	��420��$5 /Degrees of the doctor / Other title information��#��[  $��24�� . $ OnFocus=��� -J   $	 btnUpdateRefBy1��$	��100��$��Save Refd. ��$��24��$��10,0,0,0q�$y�Visible=�
�������$��0q�
������ �� $ 
Horizontal=�.+��� $��	Address :��$	��100��$
��Left=�$��Right=�  -K   $ txtRefByAddress1��$	��530��$ Address of the referral��> . #��[  $��60�� . $ OnFocus=��� $ 
Horizontal=�.+��� $��Phone No. :��$	��100��$
��Left=�$��Right=�  -L   $ txtRefByPhone1��$	��100��$ Contact phone number��#��[  $��24�� . $ OnFocus=��� $��E-Mail :��$��80��$
��Left=�$��Right=�  -M   $ txtRefByEMail1��$	��350��$ Contact email address��#��[  $��24�� . $ OnFocus=�� -N   $K GridTestItem��$��5,370,270,50q�$y�Hidden=�$	��Top=�$
��Left=�.+��� $ 
Horizontal=�.+��� $��_List :��$��60��$��Right=� �� $ lstServiceItem���� -O   $ lstServiceItem��$	��120��$	��340��
������ $	��Top=�$
��Auto=�$��12(�$��Hidden=�.@��� $��Referrer Name - 001��$	  001���� $��Referrer Name - 002��$	  002���� $��Referrer Name - 003��$	  003���� $��Referrer Name - 004��$	  004���� $��Referrer Name - 005��$	  005���� $��Referrer Name - 006��$	  006���� $��Referrer Name - 007��$	  007���� $��Referrer Name - 008��$	  008���� $��Referrer Name - 009��$	  009���� $��Referrer Name - 010��$	  010���� $��Referrer Name - 011��$	  011���� $��Referrer Name - 012��$	  012��� -P   $K GridPatientPhoto��$y�	Collapsed=����� $	C 0.4Q�$D 	LightGray��.+��� $��Center=�$��Center=�$	��800��$	��400��
���   �$��1q�
�������$E 1q�._��� ���� $F 0.5,1&�$G 0.5,0&�.��� $H 	#FFE4F2E1��$I 0.491Q�� $H 	#FF69B28B��$I 0.957Q�.+��� $ 
Horizontal=�$��
10,10,10,0q�.+�7  1System.Windows.Forms.Integration.WindowsFormsHost  -Q   M  Name$M windowsFormsHost1��$
��Left=�$	��Top=�$	��300��$	��328��$y�Visible=��� $��Center=�$	��100��$��
10,10,10,0q�.+��� -R   $	 btnTakePatPhoto��$��Capture���� -S   $	 btnUploadPatPhoto��$��Upload��$��	0,10,0,10q��� -T   $	 btnClosePatPhoto��$��Bac_k to Entry��J . �� -U   $ chkAutoClose��$��	Auto Back��$��11(�$
 TrueY�$��	0,10,0,10q��� -V   $	 btnSavePatPhoto��$��Save to Disk��$��	0,10,0,10q��� -W   $ imgPatPhotoCapture��$	��300��$	��328��$
��Fill=�
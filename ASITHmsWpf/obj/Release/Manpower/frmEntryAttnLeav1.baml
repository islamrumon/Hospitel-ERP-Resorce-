   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  'clr-namespace:ASITHmsWpf
ASITHmsWpf  +    %ASITHmsWpf.Manpower.frmEntryAttnLeav1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  $localclr-namespace:ASITHmsWpf  -   
  � Tag$>  8Staff Attendance & Leave Information - Entry/Edit Screen��   IOUserControl#��[   � 	Resources )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Property �� Value��%i   '        �� "  .���  � ��
 �������  �@=�ErrorTemplate Z� ��  � ��$ 28��.��� .+��� $��0,5,0,0q�.+���  �� Orientation$ 
Horizontal=�$��90��.+��� -    ��Name$ stkpMain��$ Vertical=�.+��� $ 
Horizontal=�.+��� $��Input :��$��80��$��Right=��� -    ��Name$ cmbInputType��$	��220�� �� 
IsReadOnly . 	 �� SelectedIndex$	 4��.@��� $"��01. MACHINE BASED ATTENDENCE��$  
01MAC_ATTN���� $��02. GROUP ATTENDENCE��$  
02GRP_ATTN���� $��03. INDIVIDUAL ATTENDENCE��$  
03IND_ATTN���� $ ��04. LEAVE APPLICATION FORM��$  04LEAV_FORM���� $��05. ATTENDENCE SCHEDULE��$  
05ATTN_SCH���� $��
Location :��$��65��$��Right=��� -   $ cmbSectCodAll��$	��280�� . $	 0���� $��Month :��$��60��$��Right=��� -   $ cmbInfoMonth��$	��125��$
��Bold"��� $��Date :��$��Right=�$��Normal$�$��45��
 �� Target
 ��  �� ElementName$ xctk_EventDateAll��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_InfoDate��  StdDateTimePickerStyle01#��[ $	��150��&   Xceed.Wpf.Toolkit.DateTimeUpDown   FormatString$ dd-MMM-yyyy ddd��$
��Bold"��� -   $ stkpEmpSearch��$ 
Horizontal=�$��0,5,0,0q�.+��� $��
Employee :��$��80��$��Right=�1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ 
AtxtEmpAll��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox   MouseDoubleClick$! AtxtEmpAll_MouseDoubleClick��   
SearchType$ C��$	��565�� � ToolTip$= 7Right Click or Double Click to view existing staff list�� � ContextMenu �� -	    ��Name$ conMenuHCMAtnAll��$	��400��$	��400���� $��	Emp. Id :��$��Right=�$��60���� -
    ��Name$ txtblAttnSchempid��$	��120��$
��Bold"�$��Center=��� $�� Card #:��$��Right=�$��50���� -   $ txtblAttnSchempIDCard��$	��150��$
��Bold"�$��Center=��� -   $ stkpMacData��$ 
Horizontal=�.+��� -    ��Name$ btnUpdateMacData��$��Update Machine Database��  StdBtnStyle01#��[ $��24��$	��150��$��80,2,0,0q��� -    ��Name$ chkMacAttnDate��$��Consider Whole Month��$��Center=�$��30,0,0,0q��� $ Vertical=�.+��� -    ��Name$ imgAttnSchEmpimg��$��../Images/NoUser.jpg��$��80��$��80��$��5,0,0,0q��� -   $ imgUserPhoto��$��../Images/NoUser.jpg��$y�Hidden=�$
��Fill=�$��0��$��0���� $ Vertical=�.+��� -   $ btnShowInfo��$
��Show��$��75��$��14(�$��24��$��Verdana)�#��[ $��5,0,0,0q��� -   $ btnUpdateInfo��$��Update��$��75��$��14(�$��24��$��Verdana)�#��[ $��5,20,0,0q��� -    ��Name$ lblTabTitle��$��Title of the selected tab��$��Center=����	#FFFFD7D7���	#FF0B37CF$
��Bold"�$��14(��� -    ��Name$ TabControl1��$	��500��$	 0��$��0q�  �� 
TargetType��%2   '��       �� " ��.���  � �� �� " ��.@��� $��Machine Based Attendence��.���� .+�� ���	#FFFFD7D7.+��� $��Machine Based Attendence��$��Center=����	#FF0B37CF$
��Bold"�$��24(��� -   $ btnPrintAttSchdlDay��#��[ $��75��$��Right=�$��14(�$��24��$��Print��$��0,0,10,0q�&   System.Windows.Controls.DataGrid  -     Name$ dgActTrmAttn��   AutoGenerateColumns .  $	��400��$
��Left=�   
IsReadOnly .    RowHeaderWidth$ 5Q�$��12(�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn    Width5	  /System.Windows.Controls.DataGridLengthConverter$	  100	 !   Header$
! Date��"   FontSize$" 10Q�# +� IsFocusScope# .  $   CanUserSort$ .  %   
IsReadOnly% . &   CanUserReorder& .  '   CanUserResize' .  1
  +System.Windows.Controls.DataGridBoundColumn( 
  Binding( �� ) �� Path$) attndat�* �� StringFormat$* {0: dd-MMM-yyyy ddd}��  $  50	 $! Emp ID��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) staffid�  $	  350	 $&!  Name of Employee and Designation��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) hcnamdsg�  $  70	 $! S.InTime��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) InTime1�  $  70	 $! S.Brk.Start��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) OutTime1�  $  70	 $! 	S.Brk.End��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) InTime2�  $  70	 $! 	S.OutTime��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) OutTime2�  $  80	 $! 	Sch. Note��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) Rmrks�  $	  280	 $! Machine Punch Details��$" 10Q�# .  $ .  % . & .  ' .  ( �� $) atndtl��� $��Group Attendence��.���� $ Vertical=�$��0,5,0,0q�.+��� $��0,5,0,0q�.+��� 
�������.+��� $��Attendence Status :��$��	ExtraBold"�$��16(�
����  �$��Italic$��� $����PP = Precfectly Present, PL = Present but Late, PE = Present and Early Leave, PX = Present, Late and Early Leave, AA = Absuluately Absent, AD = Absent due to Day-Off, AL = Absent due to Leave��$
��Bold"�  -   $ dgnm1��$	��450�� .   .  4  .System.Windows.Controls.DataGridTemplateColumn  $	! Sl#��$  40	 % . +   CellTemplate+ �� .R��� $��0q�$��35��|�.  ���� *slnum+  $
! HRID��$  80	 % . + �� .R��� $��0q�$	��170��|�.  ���� *	hccode1+  $! Name and Designation��$	  450	 + �� .R��� $ 
Horizontal=�.+��� ���� *hcname+, �� Mode$, TwoWay=�- �� UpdateSourceTrigger$- PropertyChanged=�  $! 
ID Card No��$	  150	 + �� .R��� $	��350������ *
idcardno+$, TwoWay=�$- PropertyChanged=�  �� *Code+  $! 
Att.Status��$  80	 + �� .R�(  "Xceed.Wpf.Toolkit.WatermarkTextBox   
 wtxtBx#��[ .   	Watermark$
. Attn��$��Center=�)  #Xceed.Wpf.Toolkit.AutoSelectTextBox/   AutoSelectBehavior$/ OnFocus=�$��22��$��70��$��0q����� *	atnstatus+$, TwoWay=�$- PropertyChanged=�  $! W.Hour��$  80	 + �� .R�  #��[ $
. Hour��$��Center=�$/ OnFocus=�$��22��$��70��$��0q����� *
workhour+$, TwoWay=�$* {0: ##0.00}��$- PropertyChanged=�  $! Card punch details��$	  250	 + �� .R��� $	��350������ *	cpdetails+$, TwoWay=�$- PropertyChanged=�  �� *Code+�� $��Individual Attendence��.��� $	��Top=�$	��606��$��0,0,0,-3q�.+��� .+��� $ 
Horizontal=�.+��� -   $ stkAtn1EmpInfo��
�������$	��500��$	��470��$��0,5,0,0q��� $��0,5,0,0q�.+��� 
�������.+��� $��Attendence Status :��$��	ExtraBold"�$��16(�
����  �$��Italic$��� $v��pPP = Precfectly Present, PL = Present but Late, PE = Present and Early Leave, PX = Present, Late and Early Leave��$
��Bold"��� $S��MAA = Absuluately Absent, AD = Absent due to Day-Off, AL = Absent due to Leave��$
��Bold"�  -   $ dgmlst�� .  $	��440��$	��690��$
��Left=� .    $
! Date��$	  100	 % . + �� .R��� |�.  ���� *	atndate+$* {0: dd-MMM-yyyy}��  $! 
Att.Status��$  80	 + �� .R�  #��[ $
. Attn��$��Center=�$/ OnFocus=�$��22��$��70��$��0q����� *	atnstatus+$, TwoWay=�$- PropertyChanged=�  $! 	Work Hour��$  80	 + �� .R�  #��[ $
. Hour��$��Center=�$/ OnFocus=�$��22��$��70��$��0q����� *
workhour+$, TwoWay=�$* {0: ##0.00}��$- PropertyChanged=�  $! Card punch details��$	  400	 + �� .R��� $	��380��{�.  ���� *	cpdetails+$, TwoWay=�$- PropertyChanged=�  �� *Code+�� $��Leave Application Form��.��� ���	#FFE4F2E10 � RowDefinitions0 �� $	��250 ��� $
��Auto �.+��� $ 
Horizontal=�$	��Top=�$��	10,0,10,0q�$��0��$��2��.+��� $ Vertical=�$	��560��.+�  -   $ GrdLeave�� . $��12(�$ 0Q�1   CanUserResizeRows1 .  
������ $��0q�$��0��$
��Left=�$	��Top=�$	��550��$
��Auto=�$��Hidden=�   $	  150	 $! Type of Leave��# .  $ .  % . & .  ' .  ( �� $) leavdesc�  $	  130	 $! 
Start Date��+ �� .R�  #��[ �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]2   ShowButtonSpinner2 .  $��20��$	��120��$��0q�3   Value3 �� *	begndat+$, TwoWay=�$- PropertyChanged=�  $	  130	 $! End Date��+ �� .R�  #��[ $��20��$	��120��$��0q�2 .  3 �� *enddat+$, TwoWay=�$- PropertyChanged=�  $  45	 $
! Days��+ �� .R��� $��Center=�$��Center=�$	��Top=�|�.  $��42������ *
TotalDat+  $  53	 $
! Mark��+ �� .R��� $��Center=�$��Right=�$��Center=�$��20��4 d� 	IsChecked4 �� *Mark1+$, TwoWay=�$- PropertyChanged=��� $ 
Horizontal=�.+��� -   $ btnLeaveRecal��$��Right=�$��20��$��Recalculate��$	��100��#��[ �� -   $ lblTotalDays��$��Total = 0 Day(s)��$��Right=�$
��Bold"�$	��300���� $ Vertical=�.+��� $ 
Horizontal=�$��25��$��Right=�$��0,0,5,0q�.+��� -   $ chkLeaveForm��$��Leave Application Form��$��Center=��� $��40��$y�Hidden=��� -   $ btnPrintLeave��$��Right=�$��25��$��Print��$��80��#��[ �� $��2���� $ 
Horizontal=�.+��� $��15��$y�Hidden=��� $��Application Date :��  -   $ xctk_dtpApplyDate��#��[ $��22��$	��120���� $��Approval Date :��  -    $ xctk_dtpApprovDate��#��[ $��22��$	��120���� $ 
Horizontal=�.+��� $��Reasons of Leave��$	��120��$
��Left=�$��Normal$�
 �� $ txtEditIdNO���� $��:��$��Normal$�  -!   5  Name$5 
txtLevresn��#��[ $. Reasons of leave��$��5,0,0,0q�$�� ��$
��Left=�$��22��$	��480���� $ 
Horizontal=�.+��� $��Leave Time Contacts��$	��120��$
��Left=�$��Normal$�
 �� $ txtEditIdNO���� $��:��$��Normal$�  -"   $5 
txttimecon��#��[ $.  ��$��5,0,0,0q�6 �� TextWrapping$
6 Wrap=�$�� ��$
��Left=�$��22��$	��480���� $ 
Horizontal=�.+��� $��Replacement Staff��$	��120��$
��Left=�$��Normal$�
 �� $ AtxtLeavHcCod2���� $��:��$��Normal$�  -#   $ AtxtLeavHcCod2��$ C��$
��Left=�$��22��$	��480��
���   ��� $ 
Horizontal=�.+��� $��Leave Approved By��$	��120��$
��Left=�$��Normal$�
 �� $ txtEditIdNO���� $��:��$��Normal$�  -$   $ AtxtLeavApprovBy��$ C��$
��Left=�$��22��$	��480��
���   ��� $ 
Horizontal=�.+��� $��Remarks (if any)��$	��120��$
��Left=�$��Normal$�
 �� $ txtEditIdNO���� $��:��$��Normal$�  -%   $5 txtrmrk��#��[ $. Remarks (if any)��$��5,0,0,0q�$�� ��$
��Left=�$��22��$	��480���� $ 
Horizontal=�$��0,1,15,0q�$��Right=�.+��� $��85��$y�Hidden=��� -&   $ btnLeaveReset��$��Right=�$��25��$��Clear Leave Info��$	��120��#��[ �� $��20��$y�Hidden=��� -'   $ btnLeaveUpdate��$��Right=�$��25��$��_Update Leave Info��$	��120��#��[ �� $ 
Horizontal=�$	��Top=�$
��Left=�$��	10,15,0,0q�$��1��$	��286��$
��1170��.+��� .+��� $��Leave Summary :��$��22��$
��Bold"��� -(   7 ��Name$7 	lvLeavSum��
������ $	��120��$	��700��$
��Left=�$��14(�$
��Auto=�$��Hidden=����� " ��.���  � �� �	#FFE4F2E18 �� View8 �� .P��� 9 �� Width$	9 150��$��Type of Leave��: �� DisplayMemberBinding: �� *trdesc+�� $9 80��$��	Y.Opening��; �� CellTemplate; 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��Top=�$	��100��.+��� $ txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+$* {0: #,##0.00}���� $9 60��$��Added��;   .R�� $��Right=�$	��Top=�$	��100��.+��� $ txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+$* {0: #,##0.00}���� $9 80��$��TY.Leave��;   .R�� $��Right=�$	��Top=�$��80��.+��� $ txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+$* {0: #,##0.00}���� $9 60��$��Availed��;   .R�� $��Right=�$	��Top=�$��60��.+��� $ txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+$* {0: #,##0.00}���� $9 60��$��Balance��;   .R�� $��Right=�$	��Top=�$��60��.+��� $ txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+$* {0: #,##0.00}���� $9 80��$��Applied��;   .R�� $��Right=�$	��Top=�$��80��.+��� $ txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+$* {0: #,##0.00}���� $	9 100��$��Net Balance��;   .R�� $��Right=�$	��Top=�$	��100��.+��� $ txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"����� *srfqty+$* {0: #,##0.00}���� .+��� $��Leave History:��$��20��$
��Bold"��� -)   $7 lvLeavDetail��
������ $	��120��$	��470��$��14(�$
��Auto=�$��Hidden=����� " ��.���  � �� �	#FFE4F2E18 �� .P��� $	9 150��$��Type of Leave��: �� *trdesc+�� $9 80��$
��Days��: �� *trdesc+�� $	9 100��$��
Start Date��: �� *trdesc+�� $	9 100��$��End Date��: �� *trdesc+�� $��Attendence Schedule��.��� .+��� $��5,5,5,0q�.+��� $ 
Horizontal=�.+��� .+��� $ 
Horizontal=�
�������$
��1181��.+��� $��Weekly Day Off :��$��Right=�$��99���� -*   $ cmbSchDayOff��$	 6��$	��127��$��26��$��14(�.@��� $��Saturday��$  1���� $��Sunday��$  2���� $��Monday��$  3���� $��Tuesday��$  4���� $��	Wednesday��$  5���� $��Thursday��$  6���� $��Friday��$  7��"  Xceed.Wpf.Toolkit.TimePicker  -+   <  Name$< dtpAttnSchIn1s��  StdTimePickerStyle01#��[ $��20��$��90��$��0q�$3 08:00 AMX�$��40,0,0,0q�  -,   $< dtpAttnSchOut1s��#��[ $��20��$��90��$��0q�$3 12:00 PMX�$��40,0,0,0q�  --   $< dtpAttnSchIn2s��#��[ $��20��$��90��$��0q�$3 05:00 PMX�$��40,0,0,0q�  -.   $< dtpAttnSchOut2s��#��[ $��20��$��90��$��0q�$3 11:00 PMX�$��40,0,0,0q��� $��
From Day :��$��Right=�$��70��$��10,0,0,0q�%  Xceed.Wpf.Toolkit.IntegerUpDown  -/   =  Name$= iudSchDayFrom���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]>   Minimum$> 1X�?   Maximum$? 31X�@   Value$@ 1X�$��45���� $��To Day :��$��Right=�$��63��  -0   $= iudSchDayTo��$> 1X�$? 31X�$@ 31X�$��50���� -1   $ btnSetAttnDayOffSch��#��[ $��75��$��14(�$��24��$��Assign��$��5,0,0,0q��� -2   $ btnPrintAttSchdl��#��[ $��75��$��14(�$��24��$��Print��$��40,0,0,0q��� $ 
Horizontal=�
�������$
��1181��$��0,5,0,5q�.+��� $��More Options :��$��Right=�$��99���� -3   $ cmbAssignOption��$	 0��$	��260��$��26��$��14(�.@��� $��Assign as described above��$  ASABOVE���� $��Assign like first week��$  	FIRSTWEEK���� $)��#Assign like last month (first week)��$  LASTMONTHFW���� -4   $ chkPrintActAttnWithSch��$��Print Actual Attendance��$��Center=�$��	670,0,0,0q�  -5   $ 	dgAttnSch�� .  $	��350��$
��Left=� . $ 5Q�   $
! Date��$	  120	 % . + �� .R��� |�.  ���� *
attndate+$* {0: dd-MMM-yyyy ddd}��  $! Status��$	  140	 + �� .R��� $ cmbdgAttnSch��-6   $	��140�� . $��14(�A �� TextA �� *
attnstat+$, TwoWay=�$- PropertyChanged=�.@��� $��Present��$  SIHA00501001���� $��Absent (Day Off)��$  SIHA00502002���� $��Absent (On Leave)��$  SIHA00502003��  $	  150	 $! In-Time��+ �� .R�  $ dtpdgAttnSchIn1��  StdDateTimePickerStyle02#��[ $��20��$	��150��$��0q�B   TimePickerVisibility$B Visible=�y��� *visibletime+$, TwoWay=�$- PropertyChanged=�3 �� *	intime1+$, TwoWay=�$- PropertyChanged=�  $	  150	 $! Break-Time Start��+ �� .R�  $ dtpdgAttnSchOut1��#��[ $��20��$	��150��$��0q�$B Visible=�y��� *visibletime+$, TwoWay=�$- PropertyChanged=�3 �� *
outtime1+$, TwoWay=�$- PropertyChanged=�  $	  150	 $! Break-Time End��+ �� .R�  $ dtpdgAttnSchIn2��#��[ $��20��$	��150��$��0q�$B Visible=�y��� *visibletime+$, TwoWay=�$- PropertyChanged=�3 �� *	intime2+$, TwoWay=�$- PropertyChanged=�  $	  150	 $! Out-Time��+ �� .R�  $ dtpdgAttnSchOut2��#��[ $��20��$	��150��$��0q�$B Visible=�y��� *visibletime+$, TwoWay=�$- PropertyChanged=�3 �� *
outtime2+$, TwoWay=�$- PropertyChanged=�  $! Remarks��$	  200	 + �� .R�  $	��200��$��0q����� *
attnrmrk+$, TwoWay=�$- PropertyChanged=�  $! 	Work Hour��$	  100	 + �� .R��� $	��100������ *
attnhour+$, TwoWay=�$- PropertyChanged=�y��� *visibletime+$, TwoWay=�$- PropertyChanged=�
   M S B A M L   `   `   `  ���� *  &System.Windows.Controls.Layout.ToolkitqUclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Layout.ToolkitSystem.Windows.Controls  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem E AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ASITHmsWpf
ASITHmsWpf 7 clr-namespace:ASITHmsWpf.GeneralASITHmsWpf.General  	AccordionT8clr-namespace:System.Windows.Controls;assembly=AccordionSystem.Windows.Controls    ASITHmsWpf.HmsMainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     itoolkitUclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Layout.Toolkit     9system&clr-namespace:System;assembly=mscorlib     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     *localclr-namespace:ASITHmsWpf     9localGeneral clr-namespace:ASITHmsWpf.General     MControls8clr-namespace:System.Windows.Controls;assembly=Accordion     -     �Name$  MainWindow1�� � Title$. (CentERPoint Healthcare Management System�� � Icon$ Images/Health2.ico��   HmsMainWindowStyle#��[   � 	Resources   acrMainCommand1 �� Text  acrMainCommand2��%4   &	       &	      �� $
 Text���� $
 Text�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command# [   -   # [  J� InputBindings �� 
 �� Key$ F1��	 �� Command#	 [ 
 �� CommandParameter$
 1���� $ F2��#	 [ $
 2���� $ F3��#	 [ $
 3���� $ F4��#	 [ $
 4���� $ F5��#	 [ $
 5���� $ F6��#	 [ $
 6���� $ F7��#	 [ $
 7���� $ F8��#	 [ $
 8���� $ F9��#	 [ $
 9���� $	 F10��#	 [ $
 10���� $
 Left�� �� 	Modifiers$ Shift+Controlf�#	 [ $
 ZoomIn���� $ Right��$ Shift+Controlf�#	 [ $
 ZoomOut��.��� -    �Name$ Grid1��$��0,0,0,0q� J� RenderTransformOrigin$	 0,0&����	#FFE9F5E9$	��Top=�$	��708��.+��� -    ��Name$ lblWait1��$��Please Wait.........��$y�Hidden=�$��24(�$	��272��$��50��$��Center=�$��Center=�&   System.Windows.Media.TextOptions   TextHintingMode$ Animated=�   TextFormattingMode$ Display=�$��	ExtraBold"����	#FFF20A0A$��488,301,440,357q�� -   $ UcGrid1��$
��Left=�$y�Hidden=�$	��Top=�$
��1200��$	��650��$	 0,0&�$��0,90,0,0q�.+��� $��0q� �� CornerRadius$ 8�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 5Q�   Color$ Black��   Opacity$ .8Q�   
BlurRadius$ 20Q�   RenderingBias$ Quality=�����  �� EndPoint$	 0,1&� �� 
StartPoint$	 0,1&�.���  � Color$ 	#FFECFBFB�� � Offset$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF5F6E6E��$	 1.0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF7F7F7��$	 0.0Q�� $ 	#FFCED9E4��$	 0.5Q�� $ 	#FF22F5F5��$	 1.0Q��� -   $ 	lblTitle1��$��Title of the screen��$
��Left=�$	��Top=�$
��1200��$��35��$��Center=�$��20(����	#FF862222 � ForceCursor . $
��Bold"�$��Verdana)�$��0,0,0,0q����� $ 0.5,1&� � MappingMode$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ White��� $ 	#FFB7B7B7��$
 0.46Q�� $ 	#FFDDDDDD��$ 0.233Q��� -    ��Name$ btnPrint��  btnPrint#���  $��0q�
������ |�.  $y�Hidden=�$��Right=�$��	0,0,113,0q�$	��Top=�$��32��$��32���� -	   $ btnClose��$��_Close��|�.  $��Right=�$��0,3,10,0q�$	��Top=�$��75��$��16(�$��Verdana)�  StdBtnStyle01#��[ $��25�� � ToolTip$) #Click to close the screen (Alt + C)��� -
   $ VMGrid1��$
��Left=�$��0,0,0,0q�$	��Top=�$	��250��$	��700��$	 0,0&����� $ 0.5,1&�$ 0.5,0&�.��� $ Black��� $ 	#FF17C0D2��$ 0.002Q�.+��� -     ��Name$  expMain��$��Stretch=�$��0,85,0,0q�$	��Top=�! � Cursor$
! Hand��$	��589��.+�9� -   " 9�Name$" acriFrontDesk��$�� 01. Front Desk (comm)��
����  �  StretchyExpanderTemp#��[ $��Arial Unicode Ms)�$��16(�$4 .Click here for Front Desk Commercial Operation��.���   MWacriGridStyle#��[ .+�o� -   # o�Name$# mnuFrontDesk��  MWmnu01Style#��[ .@�m� -   $ m�Name$$ mnuiFrontDesk01��$��_1. Receiption (MR)��  MWmi01Style#��[ 
% � Tag$% frmEntryFrontDesk1��& m� Icon& �� $��Images/Reception.png��$��30�� 	 MWmiImgStyle#��[	 m� -   $$ mnuiFrontDesk02��$��_2. Others Entry��#��[ $% frmEntryFrontDesk3��& �� $��Images/reportdel.png��#��[	 m� -   $$ mnuiFrontDesk03��$��_3. Help Desk��#��[ $% frmEntryHelpDesk1��& �� $��Images/Help2.png��#��[	 m� -   $$ mnuiFrontDesk04��$��_4. F.Desk Reports��#��[ $% frmReportFrontDesk1��& �� $��Images/fontDesk.png��#��[	 9� -   $" acriLab��
����  �$�� 02. Lab Reporting��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -   $# mnuLab��#��[ .@�m� -   $$ 	mnuiLab01��$��_1. Sample Receive��#��[ $)% #frmEntryLabRegister1,Sample Receive��& �� $��Images/Reportsample.png��#��[	 m� -   $$ 	mnuiLab02��$��_2. Report Preparation��#��[ $% frmEntryLabReport1��& �� $��Images/report2.png��#��[	 m� -   $$ 	mnuiLab03��$��_3. Report Submission��#��[ $,% &frmEntryLabRegister1,Report Submission��& �� $��Images/report.png��#��[	 9� -   $" 	acriStore��
����  �$�� 03. Store Operation��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -   $# mnuStore��#��[ .@�m� -   $$ mnuiStore01��$��_1. Store Requisition��#��[ $% frmEntryStoreReq1��& �� $��Images/store.png��#��[	 m� -   $$ mnuiStore02��$ ��_2. Item Issue/Consumption��#��[ $% frmEntryStoreIssue1��& �� $��Images/item3.png��#��[	 m� -   $$ mnuiStore03��$��_3. Purchase Requisition��#��[ $% frmEntryPurReq1��& �� $��Images/item1.png��#��[	 m� -   $$ mnuiStore04��$��_4. Item Receive (MRR)��#��[ $% frmEntryItemRcv1��& �� $��Images/item2.png��#��[	 m� -   $$ mnuiStore05��$��_5. Physical Stock Entry��#��[ $% frmEntryItemStock1��& �� $��Images/item.png��#��[	 m� -   $$ mnuiStore06��$��_6. Quotation Receive��#��[ $% frmEntryPurQuotation1��& �� $��Images/edit.png��#��[	 m� -   $$ mnuiStore07��$��_7. Purchase Order��#��[ $% frmEntryPurOrder1��& �� $��Images/edit.png��#��[	 m� -    $$ mnuiStore08��$��_8. Purchase Bill Receive��#��[ $% frmEntryPurBillRcv1��& �� $��Images/edit.png��#��[	 m� -!   $$ mnuiStore09��$��_9. Inventory Reports��#��[ $% frmReportStore1��& �� $��Images/item.png��#��[	 9� -"   $" acriAcc��
����  �$�� 04. Accounts��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -#   $# mnuAcc��#��[ .@�m� -$   $$ 	mnuiAcc01��$��_1. General��#��[ $% frmEntryVoucher1,General��& �� $��Images/report4.png��#��[	 m� -%   $$ 	mnuiAcc02��$��_2. Cash & Bank��#��[ $% frmEntryVoucher1,CashBank��& �� $��Images/Amount.png��#��[	 m� -&   $$ 	mnuiAcc03��$��_3. Creditors��#��[ $ % frmEntryVoucher1,Creditors��& �� $��Images/Card2.png��#��[	 m� -'   $$ 	mnuiAcc04��$��_4. Debtors��#��[ $% frmEntryVoucher1,Debtors��& �� $��Images/Card1.png��#��[	 m� -(   $$ 	mnuiAcc05��$��_5. Cheque Book Register��#��[ $% frmEntryCheqBook1��& �� $��Images/report4.png��#��[	 m� -)   $$ 	mnuiAcc06��$��_6. Accounts Reports��#��[ $% frmReportAccounts1��& �� $��Images/report5.png��#��[	 9� -*   $" acriMkt��
����  �$�� 05. Marketing��
�������#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -+   $# mnuMkt��#��[ .@�m� -,   $$ 	mnuiMkt01��$��_1. Visit Reporting��#��[ $(% "frmEntryMarketing1,Visit Reporting��& �� $��Images/clients.png��#��[	 m� --   $$ 	mnuiMkt02��$��_2. Job Assigning��#��[ $&%  frmEntryMarketing1,Job Assigning��& �� $��Images/edit.png��#��[	 m� -.   $$ 	mnuiMkt03��$��_3. Evaluation��#��[ $#% frmEntryMarketing1,Evaluation��& �� $��Images/edit.png��#��[	 m� -/   $$ 	mnuiMkt04��$��_4. Mkt. Reports��#��[ $% frmReportMarketing1��& �� $��Images/edit.png��#��[	 9� -0   $" acriHcm��
����  �$�� 06. HR & Payroll��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -1   $# mnuHcm��#��[ .@�m� -2   $$ 	mnuiHcm01��$��_1. Attendence & Leave��#��[ $% frmEntryAttnLeav1��& �� $��Images/attendance1.png��#��[	 m� -3   $$ 	mnuiHcm02��$��_2. Payroll��#��[ $% frmEntryPayroll1��& �� $��Images/Amount.png��#��[	 m� -4   $$ 	mnuiHcm03��$��_3. Recruitment��#��[ $% frmEntryRecruit1��& �� $��Images/report.png��#��[	 m� -5   $$ 	mnuiHcm04��$ ��_4. HR General Information��#��[ $% frmEntryHRGenral1��& �� $��Images/reportdel.png��#��[	 m� -6   $$ 	mnuiHcm05��$��_5. HR Reports��#��[ $% frmReportHCM1��& �� $��Images/report5.png��#��[	 9� -7   $" 	acriAdmin��
����  �$�� 07. Admin Panel��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -8   $# mnuAdmin��#��[ .@�m� -9   $$ mnuiAdmin01��$��_1. Accounts Management��#��[ $% frmEntryAccMgt1��& �� $��Images/edit.png��#��[	 m� -:   $$ mnuiAdmin02��$��_2. Inventory Management��#��[ $% frmEntryInvMgt1��& �� $��Images/edit.png��#��[	 m� -;   $$ mnuiAdmin03��$��_3. Item Rate Fixation��#��[ $% frmEntryRateFix1��& �� $��Images/edit.png��#��[	 m� -<   $$ mnuiAdmin04��$ ��_4. Purchase Req. Approval��#��[ $% frmEntryPurReqAppr1��& �� $��Images/clients.png��#��[	 m� -=   $$ mnuiAdmin05��$��_5. Admin Reports��#��[ $% frmReportAdmin1��& �� $��Images/edit.png��#��[	 9� ->   $" 	acriSetup��
����  �$�� 08. Setup & Utilities��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -?   $# mnuSetup��#��[ .@�m� -@   $$ mnuiSetup01��$��_1. Accounts Code��#��[ $% frmAccCodeBook1��& �� $��Images/clients.png��#��[	 m� -A   $$ mnuiSetup02��$��_2. Resource Code��#��[ $% frmSirCodeBook1��& �� $��Images/edit.png��#��[	 m� -B   $$ mnuiSetup03��$��_3. Other Details Code��#��[ $% frmOtherCodeBook1��& �� $��Images/edit.png��#��[	 m� -C   $$ mnuiSetup04��$��_4. Configuration Setup��#��[ $% frmConfigSetup1��& �� $��Images/edit.png��#��[	 9� -D   $" acriMis��
����  �$�� 09. MIS Reports��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -E   $# mnuMis��#��[ .@�m� -F   $$ 	mnuiMis01��$��_1. MIS Reports��#��[ $% frmReportMIS1��& �� $ ��Images/mis-report-icon.png��#��[	 m� -G   $$ 	mnuiMis02��$��_2. Doctors Ledger��#��[ $% frmReportMIS1��& �� $��Images/Doctorsl.png��#��[	 9� -H   $" acriMisc��
����  �$�� 10. Others Operation��#��[ $��Arial Unicode Ms)�$��16(�.��� #��[ .+�o� -I   $# mnuMisc��#��[ .@�m� -J   $$ 
mnuiMisc01��$ ��_1. Medicine Sales Invoice��#��[ $% frmEntryPharmaPOS1��& �� $��Images/clients.png��#��[	 m� -K   $$ 
mnuiMisc02��$"��_2. Restaurant Sales Invoice��#��[ $% frmEntryRestauPOS1��& �� $��Images/clients.png��#��[	 m� -L   $$ 
mnuiMisc03��$#��_3. Patient Visit Token Entry��#��[ $% frmEntryDocVisit1��& �� $��Images/clients.png��#��[	 m� -M   $$ 
mnuiMisc04��$ ��_4. Medicine Sales Reports��#��[ $% frmReportPharmaPOS1��& �� $��Images/clients.png��#��[	 m� -N   $$ 
mnuiMisc05��$"��_5. Restaurant Sales Reports��#��[ $% frmReportRestauPOS1��& �� $��Images/clients.png��#��[	 � -O   $ HMGrid1��$
��Left=�$	��Top=�$
��1200��$��80������ $	 1,1&�$	 0,0&�.��� $ 	#FFABF59F��$ 1Q�� $ 	#FF32B20B��$	 0.5Q�� $ 	#FF26A000��$ 0Q�.+��� -P   ' ��Name$' 	imgClIcon��$
��Left=�$	��110��$��80��$��Images/Digilab_icon.png��$
��Fill=�$��0,0,0,0q��� -Q   $' 
imgClTitle��$��	125,0,0,0q�$
��Left=�$��80��$ ��Images/Digilab_banner1.png��$
��Fill=�$	��507���� ( �� Orientation$( 
Horizontal=�$��0,0,0,54q�$��Right=�.+��� -R   $ btnMinimize��#��[ $��	_Minimize��$��66��|�.  $" Minimize Application (Alt+M)��$��23��$��0,1,0,0q��� -S   $ BtnAppClode��#��[ $��E_xit��$��60��$��Bookman Old Style)�$# Exit From Application (Alt+X)��$��16(�|�.  $��0,0,0,0q�$��23��$��	ExtraBold"��� $��Right=�$��Bottom=�$��54��$	��169��$��Images/CentERPoint3.png��$
��Fill=��� $��80��$��Right=�$	��430��$( 
Horizontal=�.+��� $
��Left=�$( Vertical=�$��0,0,0,0q�$	��180��.+��� $( 
Horizontal=�.+��� -T   $ lblSignInNamTitle�����	#FF000CFF$��Verdana)�$��12(�$��65��$��Right=�$��Sign In:���� -U   $ lblSignInNam�����	#FF000CFF$��Verdana)�$��12(�$	��115��$
��Left=�$��HAFIZ���� $( 
Horizontal=�.+��� -V   $ lblSessionIDTitle�����	#FF000CFF$��Verdana)�$��12(�$��65��$��Right=�$��Session:���� -W   $ lblSessionId�����	#FF000CFF$��Verdana)�$��12(�$	��115��$
��Left=�$��526586���� $( 
Horizontal=�.+��� -X   $ lblTeminalIDTitle�����	#FF000CFF$��Verdana)�$��12(�$��65��$��Right=�$��	Terminal:���� -Y   $ lblTeminalId�����	#FF000CFF$��Verdana)�$��12(�$	��115��$
��Left=�$��UNKNOWN���� -Z   $' imgSignInUser��$��80��$��5,0,0,0q�$��images/NoUser.jpg��$
��Fill=�$��75��� -[   $ MMGrid1��$��
250,80,0,0q�
������ $y�Visible=�.+�&   ASITHmsWpf.General.mnuFlowChart1  -\   )  Name$) mnuFlowChart1��
������ � -]   $ DevGrid1��$
��Left=�
�������$	��Top=�$
��1200��$��20��$	 0,0&�$��0, 750, 0, -70q�.+��� $��1��$	��Top=�
��� � ��� $( 
Horizontal=�.+��� $( 
Horizontal=�.+��� $
��ASIT��|�.  #��[ $��80���� $��50,2,0,0q�$( 
Horizontal=�.+��� $��12(�$��©��$��Arial Unicode MS)��� $��11(�$L��F 2014-2015 Advanced Software and IT Services Ltd. All rights reserved.��$��Arial Unicode MS)��� $��50��$��10,2,0,0q�.+��� $��11(�
����  �$��Contact:��$��Right=�$��Arial Unicode MS)��� $��5,2,0,0q�.+��� $
��Left=�$��11(�$��Arial Unicode MS)�$1��+Phone: (+88)-02-9116439, 9143472, 9118665, ��
E-Mail: �� -^   
����  ���.  $��mailto:info@asit.com.bd?�.�info@asit.com.bd	, Web: �� -_   
����  ���.  $��http://www.asit.com.bd?�.�www.asit.com.bd]� $��Right=�$	��350��.+��� -`   * ��Name$* slider1��$	��180��$
��Left=�+ � Maximum$	+ 1.5Q�, � Minimum$, 1Q�- �� TickFrequency$	- 0.2Q�. �� TickPlacement$. TopLeft=�|�.  / � Value/ �� 0 �� ElementName$0 UcGrid1��1 �� Path$1 LayoutTransform.ScaleX�2 �� Mode$2 OneWay=��� -a   3 ��Name$3 tbArrivalDateTime��$��Right=�$	��150��$��11(�$��Right=�$��0,2,0,0q�$	��Top=�(  "System.Windows.Controls.DatePicker  -b   4  Name$4 dtpMW1��$��Right=�|�.  $y�Hidden=�$��30��$��12��
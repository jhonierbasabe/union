   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   clr-namespace:PRORAMPRORAM  5   	   +clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U<clr-namespace:System.Windows.Input;assembly=PresentationCoreSystem.Windows.Input 5   	   H DShowMeTheXAML, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:ShowMeTheXAML;assembly=ShowMeTheXAMLShowMeTheXAML 5   	   #    PRORAM.Views.RadarDevicesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PRORAM.Views     5   	   'local2clr-namespace:PRORAM     5   	   ,viewsclr-namespace:PRORAM.Views     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   Jinput<clr-namespace:System.Windows.Input;assembly=PresentationCore   5   	   V	 RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	 5   	   =smtx2clr-namespace:ShowMeTheXAML;assembly=ShowMeTheXAML 	 5   	   -   5        !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . 
������    MaterialDesignBody#���   5   	   $��Medium"�5   	   $��14(�5   	   $��767.333��5   	    � 	Resources 5         PRORAM.MyConverter  MyConverter  ShadowDepth �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   	Direction   ShadowDepth   
BlurRadius	   Opacity�� 5   
   %!  &	       &	        5      6!   �� 5      " ��6(   .�5      ��  � ��
 �����60   �� 5       � ��
 �����6/   �� 5       �@J�Effect 5        5      $ White��6+   $	 320Q�69   $ 3Q�6I   $ 5Q�6Y   $		 0.5Q�6h   5       5!      5#      .  (System.Windows.Interactivity.Interaction
   Triggers
 5&      /  )System.Windows.Interactivity.EventTrigger  5(   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5)      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *ConsultDevicesCommand+5*      F  @Prism.Interactivity.InteractionRequest.InteractionRequestTrigger  5-   
   3	  -System.Windows.Interactivity.EventTriggerBase 	  SourceObject 6*   �� *(&ConfigurationChangeNotificationRequest+. 5.      +
  %Prism.Interactivity.PopupWindowAction
   
  IsModal . ! 
  CenterOverAssociatedObject .  
  WindowStyle 5/      �� 50      " �6   .�51      ��  �@�SizeToContent$ WidthAndHeight=�6:   �� 52       �@�
ResizeMode$ NoResize=�67   �� 53       �@�WindowStyle$
 None=�68   �� 54       �@�AllowsTransparency . 6?   �� 55       � ��
 ���� 67   56       
  WindowContent 58      $   PRORAM.Views.ConfigurationView  59      5;      5<        5>   
    6*   �� *TXPowerRequest+. 5?      
   .  .  5@      �� 5A      " �6   .�5B      ��  � ��$	 500��62   �� 5C       � ��$
 auto��63   �� 5D       �@�
ResizeMode$ NoResize=�67   �� 5E       �@�WindowStyle$
 None=�68   �� 5F       �@�AllowsTransparency . 6?   �� 5G       � ��
 ���� 67   5H       5J      !   PRORAM.Views.PowerRadarView  5K      5M      5N        5P   
    6*   �� *RaiseChannelFrecRequest+. 5Q      
   .  .  5R      �� 5S      " �6   .�5T      ��  � ��$	 550��62   �� 5U       � ��$	 310��63   �� 5V       �@�
ResizeMode$ NoResize=�67   �� 5W       �@�WindowStyle$
 None=�68   �� 5X       �@�AllowsTransparency . 6?   �� 5Y       � ��
 ���� 67   5Z       5\      "   PRORAM.Views.ChannelFrecView  5]      5_      5`        5b   
    6*   �� *ConfirmationRequest+. 5c      
   .  .  5d      �� 5e      " �6   .�5f      ��  �@�SizeToContent$ WidthAndHeight=�6:   �� 5g       �@�WindowStyle$
 None=�68   �� 5h       �@�AllowsTransparency . 6?   �� 5i       � ��
 ���� 67   5j       5l      (   "PRORAM.Views.PopupConfirmationView  5m      5o      5p        5s   
    6*   �� *CustomPopupRequest2+. 5t      
   .  .  5u      �� 5v      " �6   .�5w      ��  �@�SizeToContent$ WidthAndHeight=�6:   �� 5x       �@�
ResizeMode$ NoResize=�67   �� 5y       �@�WindowStyle$
 None=�68   �� 5z       �@�AllowsTransparency . 6?   �� 5{       � ��
 ���� 67   5|       5~      "   PRORAM.Views.CustomPopupView  5      5�      5�        5�   
    6*   �� *'%RadarConfigurationNotificationRequest+. 5�      
   .  .  5�      �� 5�      " �6   .�5�      ��  �@�
ResizeMode$ 	CanResize=�67   �� 5�       �@�SizeToContent$ WidthAndHeight=�6:   �� 5�       � ��$
 auto��62   �� 5�       �@�WindowStyle$
 None=�68   �� 5�       �@�AllowsTransparency . 6?   �� 5�       � ��
 ���� 67   5�       5�      )   #PRORAM.Views.RadarConfigurationView  5�      5�      5�      .��5�      ��  �� CornerRadius$ 5��6   
���iii�$��1q�65     MaterialDesignPaper#���  6I   $	��522��6|   $
��Auto��6�   $��	0,0,0,-12q�6�   ��5�   
   �� 5�      " ��6   .�5�      ��  � ��$	 800��6+    �� Triggers 5�      �� 5�       �� Binding 6"   �� *ContentControlVisibility+ �� Value 	Collapsed6O   .a�5�      ��  � ��$	 430��63   5�      5�      ._�5�   
   �  J� RenderTransformOrigin$ 	0.5,0.569&�6   $	��739��61   $	��Top=�6>    � ColumnDefinitions 5�      �� 5�      $
��auto �6#    � RowDefinitions 5�      �� 5�      $
��auto �6    �� 5�      $
��auto �6    �� 5�      $
��auto �6    �� 5�      $
��auto �6    �� 5�      $
��auto �6    .+�5�      �� $��0��6   $��0,0,10,0q�6*   .��5�      �� -   5        $��	0,0,120,0q�5�   6   ._�5�      ( 	 "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6/   $��35��6C      CornerRadius$ 5��6O   $
��Left=�6`   $	��520��6{   .��5�      �� $��Dispositivos radar��6    
�������$��Center=�6P   6o   5�      5�      5�      �� 5�      $��1��6   $��	0,0,189,0q�6*   .��5�      �� $��5q�6   $ 10��6.   
���iii�$��
10,10,0,10q�6U   ._�5�      �� .+�5�      �� -   5        $��10q�5�   !   $��Right=�6-   $��Agregar radar��6I   ��6a   �� *'%RadarconfigurationNotificationCommand+6�   5�      5�      5�      �� 5�      $��2��6   $��	0,0,129,0q�6*   .��5�      &   System.Windows.Controls.DataGrid  -   5        $��
10,10,0,10q�5�      
���iii�   AlternatingRowBackground
 �����   CanUserAddRows .     AutoGenerateColumns .  $	��238��5�         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5�      $
��Left=�6W   $	��502��6r   ��5�   /   �� *RadarDevicesModel_+  �� Mode$  TwoWay=�! �� SelectedItem! 5�      �� *SRadarDevicesModel+"   Columns" 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn#   Width$
# auto 6-   $   Header$$ Id��6:   %   
IsReadOnly% . 1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6F   �� ' �� Path$' Id�6b     5�      $
# auto 6-   $$ Nombre��6:   % . & 6J   �� $' 	RadarName�6m   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $
# auto 61   $$ Modo 2��6>   (   CellTemplate( 5�      �� 5�   "   .R�5�   &   �� ) d� 	IsChecked) 6/   �� *Check+$  TwoWay=�* �� UpdateSourceTrigger$* PropertyChanged=���5�   /   �� $+' %DataContext.GetIdRadarCheckboxCommand�+ �� RelativeSource+ �� , �� AncestorType$, DataGridL�5�   #   5�        5�      $
# auto 61   $$ Estado conexión��6>   ( 5�      �� 5�   "   .R�5�   &   ��  5�   *     ButtonStyle  MaterialDesignRaisedButton- �� BasedOn��%�  &	       7  �� 5�   .   " ��64   8-   .�5�   2   ��  � ��
 �iii�6O    5�   2   �� 5�   6    6B   �� $' Check� True6a   .a�5�   :   ��  � {� .  6V   5�   7   �� 5�   6    6B   �� $' IsSaveComplete� True6j   .a�5�   :   ��  � �� �#2196f36W   5�   7   �� 5�   6    6B   �� $' IsSaveComplete�	 False6j   .a�5�   :   ��  � ��
 �iii�6W   5�   7   5�   /   5�   +   .+�5�   *   �� #��[ 62   ��5�   2   �� $#' DataContext.GetIdRadarCommand�+ �� $, DataGridL�3 	 -MaterialDesignThemes.Wpf.ButtonProgressAssist.   IsIndicatorVisible. 5�   2   �� $' IsSaving�/   Value/ 5�   2   �� $' SaveProgress�.��5�   .   ' 	 !MaterialDesignThemes.Wpf.PackIcon  $��24��6F   $��24��6R   ��5�   2   �� 5�   6   "  6<   #- [B 6a   .�5�   :   �� � 	 �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=null]] �@ Kind$ PowerPlugOff=�6Q    5�   :   �� 5�   >    6J   �� $' IsSaveComplete� True6s   .a�5�   B   ��  �@ Kind$ 	PowerPlug=�6Y   0 N� EnterActions0 5�   B   �� 5�   F   .��5�   J   �� .��5�   N   W� 1 �� TargetProperty$1 Opacity�6^   2 W� From$2 0X�6�   	3 W� To$3 1X�6�   4 g� Duration$4 0:0:0.8D�6�   5�   K   5�   G   5�   ?   5   7   5  /   5  +   5  '   5  #   5       5	     $
# auto 61   $$ Radiando��6>   ( 5
     �� 5  "   .R�5  &   ��  5  *   ��%�  &	       7  �� 5  .   " ��64   8-   .�5  2   ��  � ��
 �iii�6O    5  2   �� 5  6    6B   �� $' Check� True6a   .a�5  :   ��  � {� .  6V   5  7   �� 5  6    6B   �� $' IsSaveCompleteT� True6k   .a�5  :   ��  � �� �#2196f36W   5  7   �� 5  6    6B   �� $' IsSaveCompleteT�	 False6k   .a�5  :   ��  � ��
 �iii�6W   5  7   5  /   5  +   .+�5  *   �� #��[ 62   ��5  )   �� $"' DataContext.RadiationCommand�+ �� $, DataGridL�. 5  )   �� $' 	IsSavingT�/ 5   )   �� $' SaveProgressT�.��5!  .     $��24��6F   $��24��6R   ��5"  2   �� 5#  6   "  6<   #- [B 6a   .�5$  :   ��  �@ Kind$ PowerPlugOff=�6Q    5%  :   �� 5&  >    6J   �� $' IsSaveCompleteT� True6t   .a�5'  B   ��  �@ Kind$ 	PowerPlug=�6Y   0 5(  B   �� 5)  F   .��5*  J   �� .��5+  N   W� $1 Opacity�6^   $2 0X�6�   $3 1X�6�   $4 0:0:0.8D�6�   5,  K   5-  G   5/  ?   51  7   53  /   54  +   55  '   56  #   58       5:     $
# auto 61   $$ Acciones��6>   ( 5;     �� 5<  "   .R�5=  &   o� 5 o� 
IsMainMenu5 .  
�������
���   �.@�5>  *   m� $��Opciones��63   $��30��6E   .@�5?  .   m� $��
Configurar��67   6 m� Command6 6K   �� $,' &DataContext.ConfigurationChangeCommand�+ �� $, DataGridL�m� 5@  .   $��Potencia��67   6 6I   �� $ ' DataContext.TXPowerCommand�+ �� $, DataGridL�m� 5A  .   $��Canales de transmisión��67   6 6W   �� $)' #DataContext.RaiseChannelFrecCommand�+ �� $, DataGridL�m� 5B  .   $��Remover��67   6 6H   �� $%' DataContext.ConfirmationCommand�+ �� $, DataGridL�5C  +   5D  '   5E  #   5G     5I     5J     �� 5L     $��3��6   $
��Left=�6Z   $
��Auto��6u   y�6*   �� *ContentControlVisibility+.��5M     �� -   5        $��	0,0,105,0q�5M  6   ._�5N       $ PrimaryDark=�6/   $��35��6C   $ 5��6O   $
��Left=�6`   $	��518��6{   .��5O     �� $��Dispositivos radar modo 2��6    
�������$��Center=�6W   6v   5P     5Q     5R     �� 5T     $��4��6   $��
0,0,100,50q�6Z   y�6*   �� *ContentControlVisibility+.��5V       -   5        $��10,10,150,30q�5V     
���iii�
 ����� .   .  $
 Auto 5\     $	��496��6W   ��5V  5   �� *RadarDevicesMode2_+$  TwoWay=�! 5W     �� *SRadarDevicesModel+" 5^       5`     $	# 100 61   $$ Id��6=   % . & 6I   �� $' Id�6e     5a     $	# 150 61   $$ Nombre��6=   % . & 6M   �� $' 	RadarName�6p     5b     $	# 200 65   $$ Orden��6A   ( 5c  "   �� 5d  &   .R�5e  *   �� $��OrdenEjecucion��62   $��1q�6I   $	��150��6U   $��25��6a   
�������$
��Left=�6�   7 �� TextAlignment$7 Center=�5f  2   
���   ���6\   �� $' Orden�$  TwoWay=�8 �� ValidatesOnDataErrors8 . 9 �� NotifyOnValidationError9 . : �� ValidatesOnExceptions: . $* 	LostFocus=�.��6�   
 5h  .     5i  2   $ TextChanged��6A   . 5j  6      6L   �� *TextChangedCommand+5k  3   .��5l  F   5m  +   5o  '   5q     5u     5w     �� 5y     $��5��6   $��	0,0,189,0q�6Z   y�6*   �� *ContentControlVisibility+.��5z     �� $��5q�6   $ 10��6.   
���iii�$��	0,271,0,0q�6U   ._�5{     �� $��0,0,0,-5q�6!   ; �� Orientation$; 
Horizontal=�63   .+�5|     �� $%��Tiempo de operacion entre radar��6    
�������$��5q�6]   $
��Left=�6h   6�   �� 5}     -   5        $��TiempoEjecucion��5}  #   $��	5,10,5,11q�6;   $��50��6N   
�������$
��Left=�6p   ) 	 #MaterialDesignThemes.Wpf.HintAssist<   Hint$
< time��5~  #   
���iii�$��2q�6_   $7 Center=�5  #   
���   ���6M   �� *TiempoEjecucion+$  TwoWay=�$* 	LostFocus=�5�  #   �� 5�     $��ms��6    
�������$��5q�6@   $
��Left=�6K   6i   �� 5�     -   5        $��10q�5�  !   $��Right=�6-   $��Activar modo 2��6I   $��30��6�   $	��132��6�   ��6b   �� *RadarconfigurationModo_2+6�   5�     5�     5�     �� 5�     $��4��6   $��Bottom=�6'   $��48��6B   $��10,0,161,-40q�6N   ��5�     �� 5�     " ��6   .�5�     ��  � ��$ 10,290,114,10q�63    5�     �� 5�      6*   �� *ContentControlVisibility+ 	Collapsed6W   .a�5�  "   ��  � ��$ 10,5,114,10q�6;   5�     5�     .+�5�     �� -	   5        $��Cerrar��5�  ;   
���   �
�������$��406,10,83,10q�6v   $��25��6�   $ -0.404,0.426&�6�   ��6   �� *CancelCommand+6�   5�     5�     5�     5�     
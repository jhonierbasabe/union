   M S B A M L   `   `   `  ����W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U<clr-namespace:System.Windows.Input;assembly=PresentationCoreSystem.Windows.Input  5   	   A =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views 5
   	   +clr-namespace:PRORAM.ViewsPRORAM.Views 5   	   clr-namespace:PRORAMPRORAM 5   	   )   #PRORAM.Views.RadarConfigurationView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   @d2http://schemas.microsoft.com/expression/blend/2008      5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   Jinput<clr-namespace:System.Windows.Input;assembly=PresentationCore   5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	   	   &localclr-namespace:PRORAM.Views  5
   	   Lcontrols9http://schemas.microsoft.com/netfx/2009/xaml/presentation     5   	   *viewsclr-namespace:PRORAM.Views     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   !local2clr-namespace:PRORAM  5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . 
������    MaterialDesignBody#���   5   	   $��Medium"�5   	   $��14(�5   	   $
��auto��5   	   $	��626��6    � 	Resources 5        PRORAM.MyConverter  MyConverter  ShadowDepth �� 
TargetType �� Property �� Value3   -System.Windows.Media.Effects.DropShadowEffect   Color   	Direction   ShadowDepth   
BlurRadius	   Opacity�� 5   
   %!  &	       &	        5      6!   �� 5      " ��6(   .�5      ��  � ��
 �����60   �� 5       � ��
 �����6/   �� 5       �@J�Effect 5        5      $ White��6+   $	 320Q�69   $ 3Q�6I   $ 5Q�6Y   $		 0.5Q�6h   5       5!      5%      .  (System.Windows.Interactivity.Interaction
   Triggers
 5'      /  )System.Windows.Interactivity.EventTrigger  5)   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5*      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *OnLoadScreenCommand+5+      F  @Prism.Interactivity.InteractionRequest.InteractionRequestTrigger  5-   
   3	  -System.Windows.Interactivity.EventTriggerBase 	  SourceObject 6*   �� *CustomPopupRequest+. 5.      +
  %Prism.Interactivity.PopupWindowAction
   
  IsModal . ! 
  CenterOverAssociatedObject .  
  WindowStyle 5/      �� 50      " �6   .�51      ��  �@�SizeToContent$ WidthAndHeight=�6:   �� 52       �@�
ResizeMode$ NoResize=�67   �� 53       �@�WindowStyle$
 None=�68   �� 54       �@�AllowsTransparency . 6?   �� 55       � ��
 ���� 67   56       
  WindowContent 58      "  PRORAM.Views.CustomPopupView  59      5;      5<        5>   
   $ Loaded��6   . 5?         6$   �� *SetIdCommand+5@      .��5C      ��  �� CornerRadius$ 10��6   
���iii�$��1q�65     MaterialDesignPaper#���  6I   ._�5D   
   �  � ColumnDefinitions 5E      �� 5F      $
��auto �6#    � RowDefinitions 5H      �� 5I      $
��auto �6    �� 5J      $
��auto �6    �� 5K      $
��auto �6    �� 5L      $
��auto �6    .+�5N      ]� $��0��6   $��0��6%   .+�5O      �� -   5        ._�5P      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6/   $��35��6C      CornerRadius$ 5��6O   .��5Q      �� $!��Registrar dispositivo radar��6    
�������$��5,0,0,0q�6Y   6l   5R      5S      5T      ]� 5V      $��1��6   $��0��6%   .+�5W      �  5X      �� 5Y      $
��auto �6(   �� 5Z      $
��auto �6(   �� 5[      $
��auto �6(   �� 5\      $
��auto �6(   �� 5]      $��* �6(   .+�5_      ]� $
��auto��6    $��0��6.   .+�5`      �� -   5        $��Label133��5`       $	��5,5q�65   $��Nombre del radar (*):��6B   $��Center=�6b   �� 5a      -   5        $��	NameRadar��5a   "   $	��5,5q�63   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$! Ingrese el nombre del radar��5c      
���iii�
���iii�$��2q�5f      $	��355��5g      ��5b      �� *$"RadarConfigurationModel_.RadarName+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError .  �� ValidatesOnExceptions .  �� UpdateSourceTrigger$ 	LostFocus=�.��5g   *    J� Effect 5h        5i   "   $ 2Q�63   $ White��6C   $	 .4Q�6Q      RenderingBias$ Performance=�6^   6|   .��5j   .   5k      5l      ]� 5m      $��1��6    .+�5n      �� -   5        $��Label1��5n       $	��5,5q�6.   $��Center=�6;   �� 5o      -   5        $��
Seleccione��5o   #   
���iii�$' !Seleccione el tipo de dispositivo��5p        �� DisplayMemberPath$
  Name��5r      $��Center=�62   $	��5,5q�6M   $	��355��6Z   ��5o   J   �� *" RadarConfigurationModel_.Modelos+! �� SelectedItem! 5q      �� *" RadarConfigurationModel_.SModelo+$ TwoWay=� .  .  . $ 	LostFocus=�
 5s        5t   "   $ DropDownClosed��61   . 5u   &      6<   �� *SelectionChangedCommand+5v   #   5x      5y      ]� 5z      $��2��6    .+�5|      �� -   5        $��Label2��5|       $��Bottom=�6W   $	��5,5q�6r   $	��210��6   " �� Target" 6.   �� # �� ElementName$# 	IpAddress��6   �� 5}      -   5        $��	IpAddress��5}   "   $	��5,5q�63   $ Ingrese la ip��5~       
���iii�
���iii�$��2q�5�      $	��201��5�      ��5      �� *$"RadarConfigurationModel_.IpAddress+$ TwoWay=� .  .  . $ 	LostFocus=�.��5�   )    5�        5�   "   $ 2Q�63   $ White��6C   $	 .4Q�6Q   $ Performance=�6^   6|   .��5�   .   5�      �� 5�      $��	20,5,10,0q�6!     MaterialDesignRaisedButton#��[ 5�      $��Bottom=�5�      4   .System.Windows.Automation.AutomationProperties$   AutomationId$$ ItemsSelectButton��5�      $��Validar��6s   ��6K   �� *SubmitRadarCommand+6s   5�      5�      5�      � 5�      $��2��6   $��0��6    .+�5�      � .+�5�      9� 
���iii�$��2q�6C   $��5,5,5,5q�5�   )   $
��auto��5�   )   $��Configuración��5�   )   
�������$��12(�5�   )   
������% 9� 
IsExpanded% 5�   )   �� *#!RadarConfigurationModel_.Expander+$ TwoWay=�.��5�        $
 Dark=�63   .��5�      �  5�   "   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $��* �64   .+�5�   "   �� $��0��61   .��5�   &   � $��5q�6+    5�   *   �� 5�   .   $
��auto �6<   �� 5�   .   $
��auto �6<    5�   *   �� 5�   .   $��* �6?   �� 5�   .   $��* �6?   .+�5�   *   ]� $��0��64   $��0��6D   $��5,5,5,1q�6Q   $��Stretch=�6b   .+�5�   .   �� -   5        $��Label3��5�   4   " 6C   �� $# Altitude��5�   +   ]� 5�   *   $��0��65   $��1��6E   $��5q�6R   $��Stretch=�6]   .+�5�   .   �� -	   5        $��Altitude��5�   8   $��1��6I   $ Ingrese la altura (m)��5�   6   
���iii�
���iii�$��2q�5�   .   $��3q�5�   .   .��69    5�   2     5�   6   $ 2Q�6G   $ White��6W   $	 .4Q�6e   $ Performance=�6r   6�   .��5�   B   .��5�   2   �� & �� Path$'& !RadarConfigurationModel_.Altitude�6:   $ 	LostFocus=�6c    . $ TwoWay=�6�    . ' �� 	Converter#' [ 5�   K   5�   3   5�   /   5�   +   ]� 5�   *   $��1��64   $��0��6D   $��5,5,5,1q�6Q   .+�5�   .   �� -
   5        $��Label4��5�   4   " 5�   ,   �� $# TXPower��5�   +   ]� 5�   *   $��1��64   $��1��6D   $��5q�6Q   .+�5�   .   �� -   5        $��TXPower��5�   6   $��2��6G   $ Ingrese la potencia��5�   4   
���iii�
���iii�$��2q�5�   .   $��3q�5�   .   .��6=    5�   2     5�   6   $ 2Q�6G   $ White��6W   $	 .4Q�6e   $ Performance=�5�   G   6e   .��5�   B   .��5�   2   �� $&&  RadarConfigurationModel_.TXPower�6:   $ 	LostFocus=�6b    . $ TwoWay=�5�   Y    . #' [ 5�   K   5�   3   5�   /   5�   +   5�   '   5�   #   �� 5�   "   $��1��61   .��5�   &   � $��5q�6+    5�   *   �� 5�   .   $
��auto �6<   �� 5�   .   $
��auto �6<    5�   *   �� 5�   .   $��1* �6?   �� 5�   .   $��1* �6?   .+�5�   *   ]� $��0��64   $��0��6E   $��5q�6R   .+�5�   .   �� -   5        $��Label5��5�   4   " 6C   �� $# InstallationAngle��5�   +   ]� 5�   *   $��0��64   $��1��6D   $��5,5,5,1q�6Q   .+�5�   .   �� -   5        $��InstallationAngle��5�   6   $��3��6P   $, &Ingrese el angulo de instalación (°)��5�   4   
���iii�
���iii�$��2q�5�   .   $��3q�5�   .   .��6:    5�   2     5�   6   $ 2Q�6G   $ White��6W   $	 .4Q�6e   $ Performance=�6r   6�   .��5�   B   .��5�   2   �� $0& *RadarConfigurationModel_.InstallationAngle�6:   $ 	LostFocus=�6l    . $ TwoWay=�6�    . #' [ 5�   K   5  3   5  /   5  +   ]� 5  *   $��1��64   $��0��6E   $��5,5,5,1q�6R   .+�5  .   �� $(��"Angulo con respecto al norte (*): ��64   5  +   ]� 5  *   $��1��64   $��1��6D   $��5,5,5,1q�6Q   .+�5	  .   �� $��8��67   $5 /Ingrese el angulo con respecto al norte (*)(°)��6D   
���iii�
���iii�$��2q�5  7   $��3q�5  7   .��6D    5  2     5  6   $ 2Q�6G   $ White��6W   $	 .4Q�6e   $ Performance=�6r   6�   .��5  B   .��5  2   �� $+& %RadarConfigurationModel_.NorthHeiding�6:   $ 	LostFocus=�6g    . $ TwoWay=�6�    . #' [ 5  W   5  3   5  /   5  +   5  '   5  #   �� 5  "   $��2��61   .��5  &   � $��5q�6+    5  *   �� 5  .   $
��auto �6<   �� 5  .   $
��auto �6<    5  *   �� 5  .   $��1* �6?   �� 5   .   $��1* �6?   5#  '   5$  #   �� 5%  "   $��3��61   .��5&  &   � $��5q�6+    5'  *   �� 5(  .   $
��auto �6<   �� 5)  .   $
��auto �6<    5+  *   �� 5,  .   $��1* �6?   �� 5-  .   $��1* �6?   .+�5/  *   ]� $��0��64   $��0��6D   $��5,5,5,1q�6Q   .+�50  .   �� -   5        $��Label9��50  4   " 6C   �� $# ChannelFrec��51  +   ]� 52  *   $��0��64   $��1��6D   $��5q�6Q   .+�53  .   �� -   5        $��ChannelFrec��53  7   $��7��6J   $' !Seleccione el canal de transmión��54  5   
���iii�$
��auto��57  -   $  DisplayName��5;  -   $��Stretch=�5<  -   {�55  -   �� *ModeloSelected+��59  -   �� *&$RadarConfigurationModel_.ChannelFrec+! 5:  -   �� *'%RadarConfigurationModel_.SchannelFrec+$ TwoWay=� .  .  . $ 	LostFocus=�5=  /   5>  +   5@  '   5A  #   �� 5B  "   $��4��61   .��5C  &   � $��5q�6+    5D  *   �� 5E  .   $
��auto �6<   �� 5F  .   $
��auto �6<   �� 5G  .   $��* �6<   .+�5J  *   �� $��0��64   $��3q�6A   ]� 5K  *   $��1��64   .+�5L  .   �� $��	4,0,-91,0q�68   �� 5M  .   $'��!Coordenadas del dispositivo radar��64   $	��282��6`   
�������6�   5N  /   5O  +   � 5Q  *   $��2��6/    5R  .   �� 5S  2   $��1* �6C   �� 5T  2   $��1* �6C   .+�5W  .   ]� $��2��68   $��0��6E   .+�5X  2   �� -   5        $��Label10��5X  8   " 6G   �� $# Latitud���� 5Z  2   -   5        $��Latitud��5Z  :   $��9��6L   $ Ingrese la latitud��5[  4   
���iii�
���iii�$��2q�5^  2   
�������$��
10,0,10,20q�5`  2   .��6G    5a  6     5b  :   $ 2Q�6K   $ White��6[   $	 .4Q�6i   $ Performance=�6v   6�   .��5c  F   .��5d  6   �� $&&  RadarConfigurationModel_.Latitud�6>   $ 	LostFocus=�6f    . $ TwoWay=�6�    . #' [ 5e  K   5g  7   5h  3   5i  /   ]� 5k  .   $��2��68   $��1��6E   .+�5l  2   �� -   5        $��Label11��5l  8   " 6H   �� $# Longitud���� 5n  2   -   5        $��Longitud��5n  :   $��10��6N   $ Ingrese la longitud��5o  4   
���iii�
���iii�
�������$��2q�5s  2   $��
10,0,10,20q�5t  3   .��6I    5u  6     5v  :   $ 2Q�6K   $ White��6[   $	 .4Q�6i   $ Performance=�6v   6�   .��5w  F   .��5x  6   �� $'& !RadarConfigurationModel_.Longitud�6>   $ 	LostFocus=�6g    . $ TwoWay=�6�    . #' [ 5y  Z   5{  7   5|  3   5}  /   5  +   5�  '   5�  #   5�     5�     5�     5�     5�     �� 5�     $��3��6   $��0��6*   .��5�     ]� $��Right=�6   .+�5�     �� $��5q�6   
�������
���   �$$ ItemsCancelButton��6R   ��6�   �� *CancelCommand+.��6�   
Cancelar6�   �� 5�     $��Aceptar��6   $��Right=�60   $��5q�6n   ��6L   �� *SubmitCommand+5�     5�     5�     5�     5�     5�     
   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PRORAMPRORAM  5   	   $    PRORAM.Views.ConfigurationView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   "localclr-namespace:PRORAM   5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . 
������    MaterialDesignBody#���   5   	   $��Medium"�5   	   $��14(�5   	   $
��auto��5   	   $	��700��6    � 	Resources 5         PRORAM.MyConverter  MyConverter  
ErrorStyle �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� Self	 �� RelativeSource
 �� Path�� 5   
   %�   &	       &	        5      6    �� 5      " �5       5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip 5      �� #	 Z $)
 #(Validation.Errors)[0].ErrorContent�5      5      5      .  (System.Windows.Interactivity.Interaction   Triggers 5"      /  )System.Windows.Interactivity.EventTrigger  5#   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5$      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *OnLoadScreenCommand+5%      .��5'      �� 
���iii�$��1q�6#    �� CornerRadius$ 5��67     MaterialDesignPaper#���  6H   ._�5)   
   �  � RowDefinitions 5*      �� 5+      $
��auto �6    �� 5,      $
��auto �6    �� 5-      $��1* �6    .+�5/      �� $��0��6   .��50      �� -   5        ._�51      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6/   $��35��6C      CornerRadius$ 5��6O   .��52      ]� .+�53      �� $&�� Configuración dispositivo radar��6$   
�������$
��Left=�6b   $��5,0,0,0q�54   $   67   55      56      57      58      � 59      $��1��6    5:      �� 5;      $
��auto �6$   �� 5<      $
��auto �6$   �� 5=      $
��auto �6$   �� 5>      $
��auto �6$   �� 5?      $��* �6$   .+�5A      �� $��0��6!   .��5B      � $��10q�6    � ColumnDefinitions 5C      �� 5D      $��1* �6/   �� 5E      $��* �6/   .+�5G      �� $��0��6%   $��	10,0,10,0q�65   .+�5H      �� -   5        $��Label3��5H   $    �� Target 63   ��  �� ElementName$ Altitude���� 5J      -   5        $��Altitude��5J   (   $��1��69   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ingrese la altitud��5K   (   
���iii�
���iii�$��2q�5N   (   $��3q�5O   (   .��63   .��5P   "   �� $$
 RadarConfigurationMod.Altitude�6*    �� UpdateSourceTrigger$ 	LostFocus=�6P    �� NotifyOnValidationError .  �� Mode$ Default=�6�    �� ValidatesOnExceptions .  �� 	Converter# [ 5Q   K   5U   #    J� Effect 5V   "   3	  -System.Windows.Media.Effects.DropShadowEffect	  5W   &    	  ShadowDepth$ 2Q�67    	  Color$ White��6G    	  Opacity$ .4Q�6U     	  RenderingBias$  Performance=�6b   6�   .��5X   2   5Z      5[      �� 5\      $��1��6%   $��	10,0,10,0q�65   .+�5]      �� -   5        $��Label4��5]   $    5^   ,   �� $ TXPower���� 5`      -   5        $��TXPower��5`   &   $��2��67   $ Ingrese la potencia��5a   4   
���iii�
���iii�$��2q�5d   .   $��3q�5e   .   .��6=    5f   "   	  5g   &   $ 2Q�67   $ White��6G   $ .4Q�6U   $  Performance=�6b   6�   .��5h   2   .��5i   "   �� $#
 RadarConfigurationMod.TXPower�6*   $ 	LostFocus=�6O    . $ Default=�6�    . # [ 5j   K   5n   #   5o      5p      5q      5r      �� 5s      $��1��6!   .��5t      � $��10q�6    5u      �� 5v      $��1* �6/   �� 5w      $��1* �6/   .+�5y      �� $��0��6%   $��	10,0,10,0q�65   .+�5z      �� -   5        $��Label5��5z   $    63   �� $ InstallationAngle���� 5|      -   5        $��InstallationAngle��5|   &   $��3��6@   $' !Ingrese el angulo de instalación��5}   4   
���iii�
���iii�$��2q�5�   .   $��3q�5�   .   .��6:    5�   "   	  5�   &   $ 2Q�67   $ White��6G   $ .4Q�6U   $  Performance=�6b   6�   .��5�   2   .��5�   "   �� $-
 'RadarConfigurationMod.InstallationAngle�6*   $ 	LostFocus=�6Y    . $ Default=�6�    . # [ 5�   K   5�   #   5�      5�      �� 5�      $��1��6%   $��	10,0,10,0q�65   .+�5�      �� $$��Angulo con respecto al norte: ��6$   �� 5�      $��8��6'   $/ )Ingrese el angulo con respecto al norte: ��5�   '   
���iii�
���iii�$��2q�5�   '   $��
10,0,10,10q�5�   '   .��6=    5�   "   	  5�   &   $ 2Q�67   $ White��6G   $ .4Q�6U   $  Performance=�6b   6�   .��5�   2   .��5�   "   �� $(
 "RadarConfigurationMod.NorthHeiding�6*   $ 	LostFocus=�6T    . $ Default=�6�    . # [ 5�   K   5�   #   5�      5�      5�      5�      �� 5�      $��2��6!   .��5�      � $��10q�6    5�      �� 5�      $��1* �6/   �� 5�      $��* �6/   5�      5�      �� 5�      $��3��6!   .��5�      � $��10q�6    5�      �� 5�      $��1* �6/   �� 5�      $��* �6/   .+�5�      �� $��0��6%   $��	10,0,10,0q�65   .+�5�      �� -   5        $��Label9��5�   $    63   �� $ ChannelFrec���� 5�      -	   5        $��ChannelFrec��5�   '   $��7��6:   $' !Seleccione el canal de transmión��5�   5   
���iii�$
��auto��5�   -   ! �� DisplayMemberPath$! DisplayName��5�   -   $��Stretch=�5�   -   {�5�   -   �� *ModeloSelected+��5�   -   �� *#!RadarConfigurationMod.ChannelFrec+" �� SelectedItem" 5�   -   �� *$"RadarConfigurationMod.SchannelFrec+$ TwoWay=�# �� ValidatesOnDataErrors# .  .  . $ 	LostFocus=�5�      5�      5�      5�      �� 5�      $��4��6!   .��5�      � $��10q�6    5�      �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��3q�61   �� 5�      $��1��6%   .+�5�      �� $��	4,0,-91,0q�6(   �� 5�   .   $&�� Cordenadas del dispositivo radar��64   $	��282��6_   
�������6�   5�      5�      � 5�      $��2��6    5�      �� 5�   "   $��1* �63   �� 5�   "   $��1* �63   .+�5�      �� $��2��6)   $��0��66   .+�5�   "   �� -
   5        $��Label10��5�   (    67   �� $ Latitud���� 5�   "   -   5        $��Latitud��5�   *   $��9��6<   $ Ingrese la latitud��5�   4   
���iii�
���iii�$��2q�5�   2   $��
10,0,10,10q�5�   2   .��6G    5�   &   	  5�   *   $ 2Q�6;   $ White��6K   $ .4Q�6Y   $  Performance=�6f   6�   .��5�   6   .��5�   &   �� $#
 RadarConfigurationMod.Latitud�6.   $ 	LostFocus=�6S    . $ Default=�6�    . # [ 5�   K   5�   '   5�   #   5�      �� 5�      $��2��6)   $��1��66   .+�5�   "   �� -   5        $��Label11��5�   (    68   �� $ Longitud���� 5�   "   -   5        $��Longitud��5�   *   $��10��6>   $ Ingrese la longitud��5�   4   
���iii�
���iii�$��2q�5�   2   $��
10,0,10,10q�5�   3   .��6I    5�   &   	  5�   *   $ 2Q�6;   $ White��6K   $ .4Q�6Y   $  Performance=�6f   6�   .��5�   6   .��5�   &   �� $$
 RadarConfigurationMod.Longitud�6.   $ 	LostFocus=�6T    . $ Default=�6�    . # [ 5�   K   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�      $��2��6   .��5�      ]� $��Right=�6   .+�5      �� $��10q�5     
�������
���   ���5     �� *CancelInteractionCommand+.��6H   
Cancelar6R   �� 5     -   5          MaterialDesignRaisedButton#��[ 5     $��10q�5     ��6%   �� *SubmitCommand+.��6]   	Aceptar6f   5     5	     5
     5     5     
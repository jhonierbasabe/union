   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U<clr-namespace:System.Windows.Input;assembly=PresentationCoreSystem.Windows.Input 5	   	   +clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   clr-namespace:PRORAMPRORAM  5   	   !    PRORAM.Views.TargetAreaView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   (viewsclr-namespace:PRORAM.Views   5   	   Jinput<clr-namespace:System.Windows.Input;assembly=PresentationCore   5	   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   &localclr-namespace:PRORAM.Views  5   	   !local2clr-namespace:PRORAM  5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . 
������    MaterialDesignBody#���   5   	   $��Medium"�5   	   $��14(�5   	   $
��auto��5   	   $	��500��6    � 	Resources 5         PRORAM.MyConverter  MyConverter  
ErrorStyle �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� Self	 �� RelativeSource
 �� Path  ShadowDepth3  -System.Windows.Media.Effects.DropShadowEffect   Color   	Direction   ShadowDepth   
BlurRadius   Opacity�� 5   
   %�  &	       &	      &	 �       5      6!   �� 5      " �5       5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip 5       �� #	 Z $2
 ,(Validation.Errors).CurrentItem.ErrorContent�5"      5$      �� 5%      " ��6(   .�5&      ��  � ��
 �����60   �� 5'       � ��
 �����6/   �� 5(       �@J�Effect 5)        5*      $ White��6+   $	 320Q�69   $ 3Q�6I   $ 5Q�6Y   $	 0.5Q�6h   5,      5-      51      .  (System.Windows.Interactivity.Interaction   Triggers 53      F  @Prism.Interactivity.InteractionRequest.InteractionRequestTrigger  54   
   3  -System.Windows.Interactivity.EventTriggerBase   SourceObject 6*   �� *CustomPopupRequest+.  (System.Windows.Interactivity.TriggerBase   Actions. 55      +  %Prism.Interactivity.PopupWindowAction     IsModal . !   CenterOverAssociatedObject .    WindowStyle 56      �� 57      " �6   .�58      ��  �@�SizeToContent$ WidthAndHeight=�6:   �� 59       �@�
ResizeMode$ NoResize=�67   �� 5:       �@�WindowStyle$
 None=�68   �� 5;       �@�AllowsTransparency . 6?   �� 5<       � ��
 ���� 67   5=         WindowContent 5?      "	   PRORAM.Views.CustomPopupView	  5@      5B      5C      .��5F      ��  �� CornerRadius$ 6��6   
���iii�$��2q�64     MaterialDesignPaper#���  6H   $
��auto��6{   ._�5G   
   � .+�5H      �� $ 6��6   
�������$��2q�69   $	��Top=�6M   $��5q�6b   ._�5I      � $��0,0,0,5q�6    � RowDefinitions 5K      �� 5L      $
��auto �6(   �� 5M      $
��auto �6(   �� 5N      $
��auto �6(   �� 5O      $
��auto �6(   �� 5P      $
��auto �6(   �� 5Q      $
��auto �6(   �� 5R      $
��auto �6(   �� 5S      $
��auto �6(   �� 5T      $
��auto �6(   .+�5V      �� $��0��6%   .��5W      �� -   5        ._�5X      (
  "MaterialDesignThemes.Wpf.ColorZone
   
  Mode$ PrimaryDark=�67   $��35��6K    
  CornerRadius$ 5��6W   .��5Y   "   �� -   5        $��Titulo��5Y   )   $#��Configuración área objetivo��67   $
��Left=�6]   
�������6�   5Z      5[      5\      �� 5]      $��1��6%   .��5^      �� $��10q�6%   .+�5_      �� -   5        $��Label1��5_   $    �� Target 64   ��  �� ElementName$ 
NombreArea���� 5a      -   5        $��
NombreArea��5a   3   $��1��6&   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$! Ingrese la nombre del área��5b   '   $��10q�5c   "   
���iii�
���iii�$��2q�5f   "   .��66    J� Effect 5g   "     5h   &   $ 2Q�67   $ White��6G   $ .4Q�6U      RenderingBias$ Performance=�6b   6�   .��5i   2   ��5j   "   �� 5k   &   $
 TargetAreaMod.NombreArea�6.     �� UpdateSourceTrigger$  	LostFocus=�6N   ! �� NotifyOnValidationError! . " �� Mode$" Default=�6�   # �� ValidatesOnExceptions# . 5p   '   .��5q   0   5r      5�      5�      �� 5�      $��5,0,5,0q�6!   $��2��62   .+�5�      �� -   5        $��Label2��5�       �� 5�      6&   5�      �� 5�      $��3��6%   .��5�      � $ � ColumnDefinitions$ 5�      �� 5�   "   $��1* �63   �� 5�   "   $��1* �63   .+�5�      �� $��10q�6*   .+�5�   "   �� -   5        $��Label3��5�   (    66   �� $ Latitud1���� 5�   "   -   5        $��Latitud1��5�   7   $��2��6*   $ Ingrese la latitud��5�   *   
���iii�
���iii�$��2q�5�   *   $��10q�5�   !   .��6.    5�   &     5�   *   $ 2Q�6;   $ White��6K   $ .4Q�6Y   $ Performance=�6f   6�   .��5�   6   ��5�   &   �� 5�   *   $
 TargetAreaMod.LatitudP1�62   $  	LostFocus=�6Q   ! . $" TwoWay=�6�   # . % �� 	Converter#% [ 5�   K   5�   +   .��5�   4   5�   #   5�      �� 5�      $��1��6)   $��10q�69   .+�5�   "   �� -   5        $��Label4��5�   (    66   �� $ 	Longitud1���� 5�   "   -	   5        $��	Longitud1��5�   7   $��3��6*   $ Ingrese la longitud��5�   '   $��10q�5�   "   
���iii�
���iii�$��2q�5�   "   .��66    5�   &     5�   *   $ 2Q�6;   $ White��6K   $ .4Q�6Y   $ Performance=�6f   6�   .��5�   6   ��5�   &   �� 5�   *   $
 TargetAreaMod.LongitudP1�62   $  	LostFocus=�6R   ! . $" Default=�6�   # . #% [ 5�   K   5�   +   .��5�   4   5�   #   5�      5�      5�      �� 5�      $��4��6%   .��5�      �� $��5,0,5,0q�6%   .+�5�      �� -
   5        $��Label6��5�   $   �� 5�      5�      5�      �� 5�      $��5��6%   5�      �� 5�      $��6��6%   .��5�      � $ 5�      �� 5�   "   $��1* �63   �� 5�   "   $��1* �63   .+�5�      �� $��10q�6*   .+�5�   "   �� -   5        $��Label7��5�   V     MaterialDesignLabel#��[ 6(    6e   �� $ Latitud2���� 5�   "   -   5        $��Latitud2��5�   7   $��4��6*   $ Ingrese la latitud��5�   "   $��10q�5�   "   
���iii�
���iii�$��2q�5�   "   .��66    5�   &     5�   *   $ 2Q�6;   $ White��6K   $ .4Q�6Y   $ Performance=�6f   6�   .��5�   6   ��5�   &   �� 5�   *   $
 TargetAreaMod.LatitudP2�62   $  	LostFocus=�6Q   ! . $" TwoWay=�6�   # . #% [ 5�   K   5�   +   .��5�   4   5�   #   5�      �� 5�      $��1��6)   $��10q�69   .+�5�   "   �� -   5        $��Label8��5�   (    67   �� $ 	Longitud2���� 5�   "   -   5        $��	Longitud2��5�   7   $��5��6*   $ Ingrese la longitud��5�   &   $��10q�5�   "   
���iii�
���iii�$��2q�5�   "   .��66    5�   &     5�   *   $ 2Q�6;   $ White��6K   $ .4Q�6Y   $ Performance=�6f   6�   .��5�   6   ��5�   &   �� 5�   *   $
 TargetAreaMod.LongitudP2�62   $  	LostFocus=�6R   ! . $" TwoWay=�6�   # . #% [ 5�   K   5�   +   .��5�   4   5�   #   5�      5�      5�      �� 5�      $��7��6    �� 5�      $��8��6%   .��5�      ]� $��Right=�6$   .+�5�      �� $��10q�6%   
�������
���   �4  .System.Windows.Automation.AutomationProperties&   AutomationId$& ItemsCancelButton��6[   ��6�   �� *CancelCommand+.��6�   
Cancelar6�   �� 5�        MaterialDesignRaisedButton#��[ 5�      $��10q�5�      $& ItemsSelectButton��6)   $��SubmitRadarCommand��6�   ��6_   �� *SubmitCommand+.��6�   	Aceptar6�   5�      5�      5      5     5     5     5     
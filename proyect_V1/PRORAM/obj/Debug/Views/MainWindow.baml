   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PRORAMPRORAM  5   	   +clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
   	   H DShowMeTheXAML, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:ShowMeTheXAML;assembly=ShowMeTheXAMLShowMeTheXAML 5   	       PRORAM.Views.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	    localclr-namespace:PRORAM  5   	   &viewsclr-namespace:PRORAM.Views  5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R	 NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5	   	   5system&clr-namespace:System;assembly=mscorlib  	 5
   	   V
 RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 
 5   	   =smtx2clr-namespace:ShowMeTheXAML;assembly=ShowMeTheXAML 
 5   	   ! 	 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    MaterialDesignPaper#���   5   	     MaterialDesignBody#���  5   	   $��Stretch=�5   	   $��Stretch=�5   	   $��Medium"�5   	   $��14(�5   	    � Title 5   	   �� *Title+��5   	   �� *&$SystemParameters.PrimaryScreenHeight+��5   	   �� *%#SystemParameters.PrimaryScreenWidth+.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *ConnectCommand+5      F 	 @Prism.Interactivity.InteractionRequest.InteractionRequestTrigger  5   
   3  -System.Windows.Interactivity.EventTriggerBase   SourceObject 6*   �� *NotificationRequest+. 5      + 	 %Prism.Interactivity.PopupWindowAction     IsModal . !   CenterOverAssociatedObject . .	 
 (MaterialDesignThemes.Wpf.ColorZoneAssist	 	  Mode$
	 Dark=�6W   5         5"   
    6*   �� *ConfirmationRequest+. 5#         .  . 65   5$        5&   
    6*   �� *ConfirmationPopUpRequest+. 5'         .  . 
   WindowStyle
 5(      �� 5)       �� 
TargetType" �6   .�5*      ��  �� Property �@�SizeToContent �� Value$ WidthAndHeight=�6:   �� 5+       � ��$
 auto��63   �� 5,       �@�WindowStyle$
 None=�68   �� 5-       �@�AllowsTransparency . 6?   �� 5.       � ��
 ���� 67   5/         WindowContent 51      (
   "PRORAM.Views.PopupConfirmationView
  52      54      55        57   
    6*   �� *LayersNotificationRequest+. 58         .  . 
 59      �� 5:      " �6   .�5;      ��  �@�SizeToContent$ WidthAndHeight=�6:   �� 5<       �@�
ResizeMode$ NoResize=�67   �� 5=       �@�WindowStyle$
 None=�68   �� 5>       �@�AllowsTransparency . 6?   �� 5?       � ��
 ���� 67   5@       5B         PRORAM.Views.LayersView  5C      5E      5F        5H   
    6*   �� *CustomPopupRequest+. 5I         .  . 
 5J      �� 5K      " �6   .�5L      ��  �@�SizeToContent$ WidthAndHeight=�6:   �� 5M       �@�
ResizeMode$ NoResize=�67   �� 5N       �@�WindowStyle$
 None=�68   �� 5O       �@�AllowsTransparency . 6?   �� 5P       � ��
 ���� 67   5Q       5S      "   PRORAM.Views.CustomPopupView  5T      5V      5W        5Y   
    6*   �� *!RadarDevicesNotificationRequest+. 5Z         .  . 
 5[      �� 5\      " �6   .�5]      ��  � �� 63   �� *&$SystemParameters.PrimaryScreenHeight+�� 5^       � �� 62   �� *%#SystemParameters.PrimaryScreenWidth+�� 5_       �@�SizeToContent$ WidthAndHeight=�6:   �� 5`       �@�
ResizeMode$ 	CanResize=�67   �� 5a       �@�WindowStyle$
 None=�68   �� 5b       �@�AllowsTransparency . 6?   �� 5c       � ��
 ���� 67   5d       5f      #   PRORAM.Views.RadarDevicesView  5g      5i      5j        5m   
    6*   �� *TargetAreaNotificationRequest+. 5n         .  . 
 5o      �� 5p      " �6   .�5q      ��  �@�
ResizeMode$ NoResize=�67   �� 5r       �@�SizeToContent$ WidthAndHeight=�6:   �� 5s       �@�WindowStyle$
 None=�68   �� 5t       �@�AllowsTransparency . 6?   �� 5u       � ��
 ���� 67   5v       5x      !   PRORAM.Views.TargetAreaView  5y      5{      5|       � 	Resources 5�         BooleanToVisibilityConverterF 
 @MaterialDesignThemes.Wpf.Converters.BooleanToVisibilityConverter ( $InvertedBooleanToVisibilityConverter   	TrueValue   
FalseValue  MenuSize �� BasedOn �� Triggers P� Property P� Value' 
 !MaterialDesignThemes.Wpf.PackIcon� 
 �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=null]]   Kind  PrimaryHueMidBrush��%s  &	       &	      '�� I     &	 �     'm� �     7m��� 5�   
   6'     5�   
   $ 	Collapsed=�6c   $ Visible=�6y   �� 5�   
   " ��6   .�5�      ��  � �� 6'   �� *DataContext.Title+5�      X�5�   
   50Q�6-   �� 5�   
   " m�6   8    5�      P� 5�       �@m�Role$ TopLevelHeader=�6*   . �5�      ��  � �� 5�      �� 5�      .R�5�   "   ]� .+�5�   &     $ MenuDown=�6>   $��Right=�6N   $��Center=�6e   #���  5�   >   �� 5�   &   $	��5,0q�6K   ��67   �� 6K   5�   #   5�      5�      �� 5�       � ��$ 0q�60   5�      5�      5�      .��5�      �  � RowDefinitions 5�   
   �� 5�      $
��auto �6   �� 5�      $��8* �6   �� 5�      $
��2.5* �6   .+�5�   
   ]� $��0��6   .+�5�      o� $	��Top=�6    o� 
IsMainMenu . .@�5�      m� $��Archivo��6   $��3q�6,   .@�5�      m� $#��Configuración área objetivo��6    m� Command 6D   �� *TargetAreaNotificationCommand+m� 5�      $��Dispositivos radar��6    6;   �� *!RadarDevicesNotificationCommand+m� 5�      $#��Administración de escenarios��6   .@�5�      m� -   5        $��Guardar escenario��5�   #   6>   m� 5�      $��Cargar escenario��6#    6=   �� *LoadStageCommand+5�      �� 5�      m� 5�      $��Salir��6    6.   �� *SaveCommand+5�      m� 5�      $��Herramientas��6   .@�5�      m� $��Regla��6   .@�5�      m� $��Punto - Punto��6#    m� CommandParameter$
 Rule��6]    6:   �� *EventosCommand+6]   5�      m� 5�      $	��Pan��6   $	 Pan��6O    6,   �� *EventosCommand+6O   m� 5�      $��	Rotación��6   $ Rotate��6T    61   �� *EventosCommand+6T   m� 5�      $��Centrar��6   $ Centrar��6S    60   �� *EventosCommand+6S   m� 5�      $��Borrar��6   $ Clear��6R    6/   �� *EventosCommand+6R   m� 5�      $
��Zoom��6   .@�5�      m� $��Acercar��6#   $ ZoomIn��6W    64   �� *EventosCommand+6W   m� 5�      $��Alejar��6#   $ ZoomOut��6V    63   �� *EventosCommand+6V   5�      5�      m� 5�      $��_Capas��6    6+   �� *LayersNotificationCommand+5�      5�      5�      �� 5�   
   $��1��6   .��5�      �  � ColumnDefinitions 5�      �� 5�      $��2* �6'   $	��450��62   �� 5�      $��10 �6'   �� 5�      $��8* �6'   �� 5�      $��10 �6'   �� 5�      $��2* �6'   $	��350��62   .+�5�      ��  �� CornerRadius$ 6��6   
�������$��2q�6=   $	��Top=�6Q   $��	10,10,5,0q�6f   ._�5�      �  5�      �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   .+�5�      �� $��0��6)   ! 	 Prism.Regions.RegionManager   
RegionName$ 
SidePanels��66   �� 5�      $��1��6)   $ 
RightPanel��66   5�      5�      �� 5�      $��1��6   $��10��6/   $��Stretch=�6:   $��2,10,2,0q�6X    �� ResizeDirection$
 Auto=�6j   �� 5�      $��2��6!   $ GeoLayer��61   $��	10,10,5,0q�5�   !   $	��300��64      RegionContext 5�   [   �� *TargetAreaMod+5�   4   �� 5�      $��3��6   $��10��6/   $��Stretch=�6:   $��2,10,3,0q�6X   �� 5�      $��4��6!   .��5�      �� $ 6��6   
�������$��2q�6A   $	��Top=�6U   $��
10,10,10,0q�6j   ._�5�      �� $ TargetPanel��6)   5�      5�      5�      5�      �� 5�   
   $��2��6   $ 6��6   
�������$��2q�6B   $	��Top=�6V   $��10q�6k   ._�5�      �� $ LogsView��6   $��5q�6H   $��Stretch=�6S   $��Stretch=�6q   5�      5�      5�      

:: config
set XSD="C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\x64\xsd.exe"
set Files=BPMN20.xsd BPMNDI.xsd DC.xsd DI.xsd Semantic.xsd
set Namespace=Polokus.Core.Interfaces.Xsd

:: execution
%XSD% /n:%Namespace% %Files% /classes

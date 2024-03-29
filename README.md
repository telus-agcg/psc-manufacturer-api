# PSC.Manufacturer.API
The PSC.Manufacturer.Api is an internal api connected to the dbo.Manufacturer table in the DCP_Main database.

## Environments
Development: [Dev swagger page](https://psc-manufacturer-api.dev.xsinc.com/PSC_manufacturer_API/v1/swagger/index.html)
<br/>
QA: [QA swagger page](https://psc-manufacturer-api.qa.xsinc.com/PSC_manufacturer_API/v1/swagger/index.html)
<br/>
UAT: [UAT swagger page](https://psc-manufacturer-api.uat.xsinc.com/PSC_manufacturer_API/v1/swagger/index.html)
<br/>
Staging: [Staging swagger page](https://psc-manufacturer-api.staging.xsinc.com/PSC_manufacturer_API/v1/swagger/index.html)
<br/>
Prod: [Prod url](https://psc-manufacturer-api.xsinc.com/PSC_manufacturer_API/v1/)
<br/>

## Healthcheck
GET /healthcheck
<br/>
Method takes no parameters. Method makes attempts to make requests to the db and write log.
<br>
Method returns healthcheck report for each attempt:
 - Name
 - Status ("Ok", or actual error message)
 - Execution time in ms.
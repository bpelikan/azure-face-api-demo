# Azure-face-api-demo
Demonstration of using Face API on Azure.  
This example uses: 
* **Function App** with **Blob trigger**, 
* **Storage account**,
* **Face API** and 
* **Azure Cosmos DB** to store result
## Setting up
What you need:
* **Function App** with:
  * `Runtime version` : `~2(Preview)`
  * `Application settings`:
    * `"ImagesStorageConnectionString": "{connection_string_to_storage_account}"`
    * `"FaceApiKey": "{face_api_key}"`
    * `"FaceApiEndpoint": "{face_api_endpoint}"`
    * `"CosmoDbConnectionString": "{connection_string_to_azure_cosmos_db}"`

* **Face API**
* **Storage account** with:
  * `Blobs Container` named `images`
* **Azure Cosmos DB** with Collection:
  * `Database id` : `facedb`
  * `Collection Id` : `images`
 

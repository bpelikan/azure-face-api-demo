# azure-face-api-demo

## Setting up
What you need:
* **Function App** with:
  * `Runtime version` : `beta`
  * `Application settings`:
    * `"ImagesStorageConnectionString": "{connection_string_to_storage_account}"`
    * `"FaceApiKey": "{face_api_key}"`
    * `"FaceApiEndpoint": "{face_api_endpoint}"`
    * `"CosmoDbConnectionString": "{connection_string_to_azure_cosmos_db}"`

* **Storage account** with:
  * `Blobs Container` named `images`
* **Face API**
* **Azure Cosmos DB** with Collection:
  * `Database id` : `facedb`
  * `Collection Id` : `images`
 

# ResizeAPI
My first shot at an API, a thumbnail maker!

Since I was developing a back end for another project of mine, 
I decided to create this API that takes in a File Name, 
a desired Size (to be converted into a squared image) and a File on a POST request (/resize) and returns an object 
with a fileName (with the word "thumb" prepended) and a fileBlob, a binary array to be used as a blob on the client side.
Of course, after having had it resized it. 

Whilst I know you can do it client-side, or using other free API's, I will be digging deeper 
into building API's in the future so this was a great excuse to get my feet wet.



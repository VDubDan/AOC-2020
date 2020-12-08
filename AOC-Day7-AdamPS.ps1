function CanBagContainColour {
[CmdletBinding()]
    param (
        [ValidateNotNullOrEmpty()]
        [string] $Colour,

        [ValidateNotNullOrEmpty()]
        [object] $Bag,
   
        [ValidateNotNullOrEmpty()]
        [object] $BagArrau
    )

    $contents = $Bag.bagContents
    $bagColour = $Bag.bagColour

    foreach($content in $contents){

    [string]$content = $content.Trim() 

    if($content -eq $Colour) {

    Write-Host "ContainsColour"
    return $true
    
    }

   
    }
  


    Write-Host "Couldn't find shiny gold directly in $bagColour, checking sub bags"

    foreach($content in $contents){
    [string]$content = $content.Trim() 
    
    
     # Find the bag
        foreach ($allBag in $BagArrau){
         
         $contentColour = $allBag.bagColour

         if($contentColour -eq $content) {

         Write-Host "Checking sub bag: $content"
         $result = CanBagContainColour -Colour $Colour -Bag $allBag -BagArrau $BagArrau
         
         if($result -eq $true) { return $true }

         }
         

         }

        



    }



    
  




}






# get input data as array
[array]$inputData = Get-Content input.txt

# set variables
$bags = @()

# iterate through array to create bags array
foreach($rule in $inputData){
    # get bag colour
    $bagColour = $rule -replace ' bags.*' 
    $bagColour = $bagColour.Trim()


    # only add if doesnt already exist
    if($bags | Where-Object {$_.bagColour -eq $bagColour}){break}

    # get bag contents
    if($rule -match 'no other bags'){
        $bagContents = $null
    }
    else{
        $bagContents = $rule -replace '.* contain ' -replace 'bags?' -replace '\.' -replace '[0-9]*'  -split ','

    }

    # create temp table to add to overall array
    $bagsTemp = '' | Select-Object bagColour,bagContents
    $bagsTemp.bagColour = $bagColour
    $bagsTemp.bagContents = $bagContents

    $bags += $bagsTemp
}

# get empty bags into an array
$emptyBags = $bags | Where-Object {$_.bagContents -eq $null} | Select-Object -ExpandProperty bagColour

# remove empty bags from list
$bags = $bags | Where-Object {$_.bagContents -ne $null}

# iterate through bags array to remove empty bags
#for($i=0; $i -lt $bags.Count; $i++){
#    # get bag contents
#    $bagContents = $bags[$i].bagContents

    # get bag colour
#    $bagColour = $bags[$i].bagColour

    # remove empty bags
#    foreach($bagContent in $bagContents){
#        $bagContentColour = $bagContent -replace '[0-9]'
#        $tempArray = @()

 #       if($emptyBags -contains $bagContentColour){
 #           $bagContentTemp = $bagContent | Where-Object {$_ -match $bagContentColour}
 #           $tempArray += $bagContentTemp
 #      }
 #       ($bags | Where-Object {$_.bagColour = $bagContentColour}).bagContents = $tempArray
 #   }
# }

### everything below this is a fucking disaster and i dont know what im doing


$count = 0

foreach($bag in $bags){

$bagCol = $bag.bagColour

Write-Host "Checking $bagCol"

$result = CanBagContainColour -Colour "shiny gold" -Bag $bag -BagArrau $bags

#Write-Host "Checked $bag.bagColour : $result"

if($result -eq $true) {

$count++

}




}

Write-host $count



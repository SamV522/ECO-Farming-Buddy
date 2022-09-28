# ECO Farming Buddy
Handle little companion tool for farmers in the game [ECO](https://store.steampowered.com/app/382310/Eco/), designed to help you work out what will grow in each 5x5 chunk
 
## How To Use
1. click the ... button on the right of the text box at the top
2. select your agricultural data, or use the sample provided and hit ok
3. DATA!
4. Use a soil sampler in-game to find the Temperature and Rainfall of your desired 5x5 chunk (hover your land claim stake over the ground to see the grid).
5. Input the temperature as a decimal (e.g. xx.yy), and and the rainfall as percentage without the percent sign (e.g. 54)
6. Tick filter crops.
7. Et Voilà!  A list of crops that *should** grow in your desired 5x5 chunk, sort by Suitability for best* fit.
<sup>*subject to agricultural data accuracy</sup>

### Plantable Only
Show only crops that have seeds that can be obtained
### Optimal Only
Show only filtered crops that are within their optimal ranges
### Enable Editing*
<sup>*Work in progress, make a backup of your data before using this.</sup>

This option will allow you to double click a cell in the grid and edit the data.  Good for updating your agricultural data on the fly.  Just don't forget to save.

## Notes
* Suitability is only a loose guide to what crop matches the parameters the most.
* if you update the temperature/rainfall after ticking on Filter Crops, you will have to click Refresh Filters to get the right data.
* does not support pollution/fertilizer (yet), just look after your crops.
* sample data provided in csv is not guaranteed to be correct, as it was poorly retrieved from the [wiki](https://wiki.play.eco/en/Agriculture#Crop_temperature_and_moisture_preferences)
* accuracy not guaranteed, but it should be close enough in most cases.

## Changes
**Full Changelog**: https://github.com/SamV522/ECO-Farming-Buddy/commits

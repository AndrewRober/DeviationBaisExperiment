# DeviationBaisExperiment

**The practical and theoretical effectiveness of Bessel’s correction for correcting bias in estimating sample variance and standard deviation**

Andrew Rober

<Andrew_Rober@hotmail.com>


**Abstract.** This research paper presents an experiment that tests the practical and theoretical effectiveness of Bessel’s correction for correcting bias in sample variance and standard deviation calculations, First experiment we tested the correction on randomly generated datasets of constant range; various sizes with randomly selected sample sets; we sample the datasets few times randomly using a low initial percentage and incrementing said sample size to observe both the biased and non-biased estimations for that population sample and their absolute deviation from the calculated standard deviation of the dataset. With the second and third experiment to explain the findings of the first experiment as we isolated the two effective variables, we found which are the dataset size in absolute and the samples size in relative and in absolute.

We’ve fixed the seeds in the random generators used; to make sure we can replicate the same results when required (for instance changing the sampling resolution/frequency and/or changing the sets size).

**First Experiment:**

We generated 3 datasets of sizes (1k, 10k and 1kk) and populated them with random numbers of range (min:0 -> max:1000), the 3 datasets were generated in a way to exclude outliers, and have a uniform distribution across the entire range of 999, with the mean and median averaging around 500, and the Quartiles averaging almost 250 for Q1 and almost 750 for Q3 that tended to achieve the actual percentages as result of the uniform distribution following the law of large numbers.


||**SET SIZE**|**DEVIATION**|**VARIANCE**|**MEAN**|**MEDIAN**|**Q1**|**Q3**|**IQR**|**RANGE**|
| :- | :- | :- | :- | :- | :- | :- | :- | :- | :- |
|**SMALL**|1000|289\.95|84068\.23|491\.78|496|236|735|499|999|
|**MEDIUM**|10,000|288\.91|83468\.97|497\.12|497\.5|246|745\.5|499\.5|999|
|**LARGE**|1,000,000|288\.52|83243\.83|499\.38|499|250|749|499|999|

Sampling was done using a percentage of the dataset, starting at 0.5%, with an increment of 0.5% with 20 samples with the highest of 10%

> *Sampling 20 times; [0.005, 0.01, 0.015, 0.02, 0.025, 0.03, 0.035, 0.04, 0.045, 0.05, 0.055, 0.06, 0.065, 0.07, 0.075, 0.08, 0.085, 0.09, 0.095, 0.1]*
>
> *Generating samples...*
>
> *Actual Small Set Sampling Counts [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100]*
>
> *Actual Medium Set Sampling Counts [50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000]*
>
> *Actual Large Set Sampling Counts [5000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 55000, 60000, 65000, 70000, 75000, 80000, 85000, 90000, 95000, 100000]*

**Results.**

For the small dataset

|**SAMPLE SIZE**|**SAMPLE PERCENTAGE**|**ACTUAL STD**|**BIASED STD**|**NON-BIASED STD**|**BIASED AD**|**NON-BIASED AD**|
| :-: | :-: | :-: | :-: | :-: | :-: | :-: |
|**5**|0\.50%|289\.95|291\.17|260\.43|1\.23|29\.51|
|**10**|1%|289\.95|267\.31|253\.59|22\.63|36\.35|
|**15**|1\.50%|289\.95|315\.1|304\.42|25\.16|14\.47|
|**20**|2%|289\.95|334|325\.54|44\.05|35\.6|
|**25**|2\.50%|289\.95|265\.53|260\.16|24\.42|29\.78|
|**30**|3%|289\.95|271\.55|266\.98|18\.4|22\.96|
|**35**|3\.50%|289\.95|289\.86|285\.69|0\.09|4\.26|
|**40**|4%|289\.95|327\.28|323\.17|37\.34|33\.22|
|**45**|4\.50%|289\.95|303\.33|299\.94|13\.38|9\.99|
|**50**|5%|289\.95|287\.19|284\.3|2\.76|5\.65|
|**55**|5\.50%|289\.95|293\.82|291\.13|3\.87|1\.19|
|**60**|6%|289\.95|267\.39|265\.15|22\.55|24\.79|
|**65**|6\.50%|289\.95|285\.69|283\.49|4\.25|6\.46|
|**70**|7\.00%|289\.95|280\.99|278\.98|8\.95|10\.96|
|**75**|7\.50%|289\.95|299\.86|297\.86|9\.92|7\.91|
|**80**|8%|289\.95|298\.97|297\.1|9\.03|7\.15|
|**85**|8\.50%|289\.95|288\.09|286\.39|1\.86|3\.56|
|**90**|9%|289\.95|277\.08|275\.53|12\.87|14\.41|
|**95**|9\.50%|289\.95|280\.8|279\.31|9\.15|10\.63|
|**100**|10%|289\.95|287\.23|285\.79|2\.72|4\.16|

Plotting the results with and without log scale shows that Bessel’s correction was significant at few points (most obvious at 0.5% with 28 points absolute deviation difference) then starts overshooting to cause more error than without it; and it then went almost identical after 2%

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 001](https://user-images.githubusercontent.com/54873972/233227652-d370e2d9-4718-480f-8b39-714c948bef5f.png)
![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 002](https://user-images.githubusercontent.com/54873972/233227663-f00aca8e-87ec-4fde-b579-0a1631330d03.png)


**Figure 1.1, 1.2** showing the estimated std biased and non-biased with the actual calculated standard deviation of the entire population; figures are identical except that 1.2 was on a Log scale to emphasis the differences (the closer values are to the calculated standard deviation of the entire set the better)

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 003](https://user-images.githubusercontent.com/54873972/233227689-622dc3c9-cc5b-4313-8306-fb2001955e77.png)

**Figure 1.3** showing the Biased vs non-biased absolute deviation (the lower the better)

With the medium dataset of size 10k



|**SAMPLE SIZE**|**SAMPLE PERCENTAGE**|**ACTUAL STD**|**BIASED STD**|**NON-BIASED STD**|**BIASED AD**|**NON-BIASED AD**|
| :-: | :-: | :-: | :-: | :-: | :-: | :-: |
|**50**|0\.50%|288\.91|284\.59|281\.73|5\.36|8\.22|
|**100**|1%|288\.91|289\.59|288\.13|0\.36|1\.81|
|**150**|1\.50%|288\.91|287\.42|286\.46|2\.52|3\.48|
|**200**|2%|288\.91|271\.65|270\.97|18\.3|18\.98|
|**250**|2\.50%|288\.91|296\.43|295\.84|6\.49|5\.89|
|**300**|3%|288\.91|281\.81|281\.34|8\.13|8\.6|
|**350**|3\.50%|288\.91|294\.74|294\.32|4\.8|4\.38|
|**400**|4%|288\.91|292\.92|292\.56|2\.98|2\.61|
|**450**|4\.50%|288\.91|294\.26|293\.93|4\.32|3\.99|
|**500**|5%|288\.91|291\.9|291\.61|1\.96|1\.67|
|**550**|5\.50%|288\.91|285\.72|285\.46|4\.23|4\.49|
|**600**|6%|288\.91|279\.45|279\.21|10\.5|10\.73|
|**650**|6\.50%|288\.91|295\.05|294\.82|5\.1|4\.88|
|**700**|7\.00%|288\.91|292\.16|291\.95|2\.21|2\.01|
|**750**|7\.50%|288\.91|291\.09|290\.9|1\.15|0\.95|
|**800**|8%|288\.91|296\.64|296\.45|6\.69|6\.51|
|**850**|8\.50%|288\.91|284\.62|284\.45|5\.33|5\.5|
|**900**|9%|288\.91|288\.55|288\.39|1\.4|1\.56|
|**950**|9\.50%|288\.91|285\.43|285\.28|4\.52|4\.67|
|**1000**|10%|288\.91|284\.62|284\.47|5\.33|5\.47|


It followed that pattern slightly, yet the difference of the estimated biased and non-biased absolute deviation from the actual calculated standard deviation was very minimal in absolute, yet relatively the very first data point tended to follow a similar pattern.

Our initial conclusion from this set was that Bessel’s correction was getting less insignificant the bigger the dataset gets, and/or the bigger the sample gets.

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 004](https://user-images.githubusercontent.com/54873972/233227728-646f791f-74d3-46ec-814a-8a73fbb64573.png)


**Figure 1.4**, Same as figure 1.2 the Log scale of the plotted actual std, biased std and non-biased std.


![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 005](https://user-images.githubusercontent.com/54873972/233227736-3a2cb395-aa63-4788-96e2-e55b66d3770f.png)


**Figure 1.5**, showing the Biased vs non-biased absolute deviation (the lower the better)



**For the Large dataset of size 1,000,000**

|**SAMPLE SIZE**|**SAMPLE PERCENTAGE**|**ACTUAL STD**|**BIASED STD**|**NON-BIASED STD**|**BIASED AD**|**NON-BIASED AD**|
| :-: | :-: | :-: | :-: | :-: | :-: | :-: |
|**5000**|0\.50%|288\.52|290\.47|290\.44|0\.52|0\.49|
|**10000**|1%|288\.52|291\.91|291\.89|1\.96|1\.95|
|**15000**|1\.50%|288\.52|287\.48|287\.47|2\.47|2\.48|
|**20000**|2%|288\.52|289\.64|289\.63|0\.31|0\.32|
|**25000**|2\.50%|288\.52|287\.05|287\.04|2\.9|2\.9|
|**30000**|3%|288\.52|286\.31|286\.3|3\.64|3\.64|
|**35000**|3\.50%|288\.52|288\.06|288\.05|1\.89|1\.89|
|**40000**|4%|288\.52|288\.19|288\.19|1\.75|1\.76|
|**45000**|4\.50%|288\.52|288\.8|288\.79|1\.15|1\.15|
|**50000**|5%|288\.52|288\.66|288\.66|1\.29|1\.29|
|**55000**|5\.50%|288\.52|288\.4|288\.4|1\.54|1\.54|
|**60000**|6%|288\.52|288\.36|288\.36|1\.59|1\.59|
|**65000**|6\.50%|288\.52|288\.82|288\.82|1\.12|1\.13|
|**70000**|7\.00%|288\.52|288\.32|288\.32|1\.62|1\.63|
|**75000**|7\.50%|288\.52|288\.69|288\.69|1\.25|1\.25|
|**80000**|8%|288\.52|288\.1|288\.1|1\.84|1\.84|
|**85000**|8\.50%|288\.52|288\.6|288\.59|1\.35|1\.35|
|**90000**|9%|288\.52|289\.04|289\.04|0\.91|0\.91|
|**95000**|9\.50%|288\.52|288\.07|288\.06|1\.88|1\.88|
|**100000**|10%|288\.52|289\.03|289\.03|0\.92|0\.92|


![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 006](https://user-images.githubusercontent.com/54873972/233227756-e7c040ae-a5c5-403e-b975-5838d32372b3.png)


**Figure 1.6**, showing the Log scale of the plotted actual std, biased std and non-biased std.

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 007](https://user-images.githubusercontent.com/54873972/233227767-945cb6b9-d3e5-41bc-abba-eaeeb6d90ce4.png)

**Figure 1.7**, showing the Biased vs non-biased absolute deviation (the lower the better)

The third dataset confirmed our initial conclusion, that while the percentage of sampling of the dataset is constant, we had two variables that can contribute to such behavior are the dataset size and the sampling size in absolute and of course relative to the same dataset.



**Second experiment:**

The purpose of this experiment to show the relation between varying the dataset size using a constant sample set (20 sets, like the first experiment) except this time we used the same sampling counts of 5 with 5 increments up to 100; using the Biased and Non-Biased AD difference to calculate MAD (Mean Average Deviation), MSE (Mean Square Error) and MAPE (Mean Absolute Percentage Error)


![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 008](https://user-images.githubusercontent.com/54873972/233227788-79eb39bc-cf8c-4b83-8470-3ce7aace8976.png)

**Figure 1.8** showing the relation between the MAPE (Mean Absolute Average Error) with its trend line 

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 009](https://user-images.githubusercontent.com/54873972/233227799-0048cd04-b4d4-4a55-b41e-0b25d557d85c.png)

**Figure 1.9** showing the MAD (Mean Average Deviation) [results have not been divided by the numbers of samples 20].

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 010](https://user-images.githubusercontent.com/54873972/233227825-55e5a040-f4f2-44f7-96dc-f2d82f3690be.png)

**Figure 2.0**, showing the MSE (Mean Squared Error) with its trendline.

We’ve done this experiment with datasets of size 1000 all the way to 200,000 and the results where very much constant, we’ve always had the MAD for the sum of difference between the biased and non-biased over the mean of 20 samples to always end up at around 80-100/20 with the initial set of 5 contributing to around 30-50% of that.


|**DATASET SIZE**|**MAD**|**MSE**|**MAPE**|
| :-: | :-: | :-: | :-: |
|**1000**|5\.03|71\.43|1\.87%|
|**2000**|5\.83|110\.76|1\.53%|
|**3000**|5\.65|66\.99|1\.87%|
|**4000**|5\.21|38\.91|1\.98%|
|**5000**|5\.83|83\.48|1\.90%|
|**…**|…|…|…|
|**195000**|4\.59|37\.42|1\.81%|
|**196000**|4\.81|85\.91|1\.94%|
|**197000**|5\.57|51\.47|1\.95%|
|**198000**|4\.55|104\.96|1\.56%|
|**199000**|5\.21|50\.39|1\.98%|
|**200000**|5\.1|81\.65|1\.89%|


**Experiment 3:**

We’ve repeated element two, but instead of fixing the sample size across different dataset sizes, we went back to the percentage model we used in the first experiment. Which means we use a percentage of the dataset size rather than fixed sample size to yield a bigger sample size for bigger datasets.


|**DATASET SIZE**|**MAD**|**MSE**|**MAPE**|
| :-: | :-: | :-: | :-: |
|**1000**|5\.49|53\.63|1\.80%|
|**2000**|2\.7|17\.61|0\.82%|
|**3000**|1\.85|6\.8|0\.58%|
|**4000**|1\.27|3\.85|0\.47%|
|**5000**|1\.03|2\.39|0\.34%|
|**6000**|0\.87|1\.78|0\.31%|
|**7000**|0\.78|1\.44|0\.25%|
|**8000**|0\.63|0\.93|0\.23%|
|**9000**|0\.59|0\.68|0\.20%|
|**10000**|0\.51|0\.62|0\.18%|
|**11000**|0\.48|0\.54|0\.17%|
|**12000**|0\.42|0\.55|0\.15%|
|**13000**|0\.39|0\.38|0\.14%|
|**14000**|0\.37|0\.38|0\.13%|
|**15000**|0\.35|0\.32|0\.12%|
|**16000**|0\.33|0\.27|0\.11%|
|**17000**|0\.31|0\.27|0\.11%|
|**…**|…|…|…|
|**195000**|0\.03|0|0\.01%|
|**196000**|0\.03|0|0\.01%|
|**197000**|0\.03|0|0\.01%|
|**198000**|0\.03|0|0\.01%|
|**199000**|0\.03|0|0\.01%|
|**200000**|0\.03|0|0\.01%|


![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 011](https://user-images.githubusercontent.com/54873972/233227899-f88c8baa-c6dd-412a-9ff3-71c58148e0ab.png)

**Figure 2.1**, showing the MAD (Mean Average Deviation) vs the dataset size.

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 012](https://user-images.githubusercontent.com/54873972/233227916-3793a9ee-6797-4d2c-bb6e-8131a75350d8.png)

**Figure 2.2**, showing the MSE (Mean Squared Error) with its trendline. vs the dataset size

![Aspose Words a5c32a1f-1019-41bc-9a07-59cbd8217fdc 013](https://user-images.githubusercontent.com/54873972/233227930-95c38db0-5b66-42d0-94a7-08cf3d5ba22d.png)

**Figure 2.3**, showing the relation between the MAPE (Mean Absolute Average Error) with its trend line vs the dataset size.

Our initial take from this third experiment was that the more samples we have in absolute, the less effective Bessel’s correction becomes as the estimated deviation starts being identical with and without using the correction, which was obvious starting with samples sizes of more than 30 samples on a dataset of 1000. 

**Conclusion**. 

Bessel's correction is effective to correct the bias in estimating small sample sizes of less than 30 samples with the tendency to overshoot, with few cases of at 15-30 samples that we've seen better results when not correcting for bias. The effectiveness of Bessel's correction seems to be having a linear relation with the size of the sample in absolute while little to no effect in relative with the dataset size. while the dataset size had very little to no effect when having a fixed range of 999 that we used in this calculation given that we had a very uniform distribution with no outliers; we suspect that this might not be the case if we extended the range as the dataset size increased but that was outside the focus of this research at this point in time.
2


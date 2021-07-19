#!/bin/bash
#SBATCH -J GUESS_NCPS
#SBATCH --error=%j.err
#SBATCH --output=%j.log
#SBATCH -D ./
#SBATCH --get-user-env
#SBATCH --clusters=cm2_tiny
#SBATCH --partition=cm2_tiny
#SBATCH --nodes=1
#SBATCH --ntasks-per-node=1
#SBATCH --mail-type=ALL
#SBATCH --mail-user=papa@tum.de
#SBATCH --export=NONE
#SBATCH --time=01:20:00
  
module load slurm_setup
current_dir=\$PWD
rm -r *run/run*
rm -r *.z*
/dss/dsshome1/lxc03/ga92wol2/Utils/DriverConversion/build/Converter ""
zip -s 1g -r Output.zip *.*
for file in *.z*
do
    curl --ciphers AES256-SHA -T $file -u 'ga92wol:Aemwded1!REAL' https://webdisk.ads.mwn.de/hcwebdav/TUWZ/b7a/data/Papastefanou/Simulations/Temporary/$file 
done; # file

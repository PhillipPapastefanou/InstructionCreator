#!/bin/bash
#SBATCH -J GUESS_NCPS
#SBATCH --error=%j.err
#SBATCH --output=%j.log
#SBATCH -D ./
#SBATCH --get-user-env
#SBATCH --clusters=cm2
#SBATCH --partition=cm2_std
#SBATCH --nodes=12
#SBATCH --ntasks-per-node=16
#SBATCH --mail-type=ALL
#SBATCH --mail-user=papa@tum.de
#SBATCH --export=NONE
#SBATCH --time=18:00:00
  
module load slurm_setup
 
mpiexec -n $SLURM_NTASKS /dss/dsshome1/lxc03/ga92wol2/LPJ-GUESS/2021/1.7/build/guess -parallel -input ncps -mdg Insfiles.txt
 

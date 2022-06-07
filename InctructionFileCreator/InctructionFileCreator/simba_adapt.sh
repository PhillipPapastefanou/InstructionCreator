NPROCESS=24
WALLTIME=20:00:00
MAILTYPE='FAIL'


# Create SLURM script to request place in queue
cat <<EOF > guess.cmd
#!/bin/bash
#SBATCH -n $NPROCESS
#SBATCH --time=$WALLTIME
#SBATCH --mail-type=$MAILTYPE
#SBATCH -o "slurm-%j.log"
set -e
mpirun /home/phillip/git/guess4.1_hydraulics/build/guess -parallel -input ncs -mdi Insfiles.txt

EOF

# Submit guess job
append_dependency=$(sbatch -J ${name:-"guess"} guess.cmd | awk '{print $NF}')
